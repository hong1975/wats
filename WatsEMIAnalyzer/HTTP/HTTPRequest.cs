using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Web;

namespace WatsEMIAnalyzer.HTTP
{
    class HTTPRequest
    {
        public static int BUFFER_SIZE = 10*1024;

        public static HttpStatusCode MakeRequest(string aUrl, string aMethod, 
            string aAccept, string aContentType,
            byte[] aReqBody, out byte[] aRespData, 
            out WebHeaderCollection respnseHeaders
            /*,int connectTimeout, int readWriteTimeout*/)
        {
            //aUrl = HttpUtility.UrlPathEncode(aUrl);
            aRespData = null;
            respnseHeaders = null;

            HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(aUrl);
            //request.Timeout = connectTimeout;
            //request.ReadWriteTimeout = readWriteTimeout;

            request.CookieContainer = HTTPAgent.CookieContainer;
            if (!string.IsNullOrEmpty(HTTPAgent.Proxy))
            {
                WebProxy proxy = new WebProxy(HTTPAgent.Proxy, false);
                request.Proxy = proxy;
            }
            else
            {
                request.Proxy = null;
            }
            ServicePointManager.ServerCertificateValidationCallback =
                new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);

            Stream reqStream = null;
            Stream respStream = null;
            HttpWebResponse response = null;
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            try
            {
                request.UserAgent = HTTPAgent.UserAgent;
                request.Method = aMethod;
                if (!string.IsNullOrEmpty(aAccept))
                    request.Accept = aAccept;
                if (!string.IsNullOrEmpty(aContentType))
                    request.ContentType = aContentType;

                CredentialCache myCache = new CredentialCache();
                myCache.Add(new Uri(aUrl), "Digest", new NetworkCredential(HTTPAgent.Username, HTTPAgent.Password));
                request.Credentials = myCache;

                if (aReqBody != null 
                    && (string.Compare("PUT", aMethod, true) == 0 || string.Compare("POST", aMethod, true) == 0))
                {
                    reqStream = request.GetRequestStream();
                    using (BinaryWriter br = new BinaryWriter(reqStream))
                    {
                        br.Write(aReqBody);
                    }
                }

                response = (HttpWebResponse)request.GetResponse();
                statusCode = response.StatusCode;
                
                byte[] tmp;
                byte[] tmpResponseData;
                //if (statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.Accepted)
                //{
                    using (BinaryReader br = new BinaryReader(response.GetResponseStream()))
                    {
                        int len;
                        do 
                        {
                            if (response.ContentLength > 0)
                                tmp = new byte[response.ContentLength];
                            else
                                tmp = new byte[BUFFER_SIZE];

                            len = br.Read(tmp, 0, tmp.Length);
                            if (aRespData == null)
                            {
                                aRespData = new byte[len];
                                Array.Copy(tmp, aRespData, len);
                            }
                            else
                            {
                                tmpResponseData = aRespData;
                                aRespData = new byte[tmpResponseData.Length + len];
                                Array.Copy(tmpResponseData, aRespData, tmpResponseData.Length);
                                Array.Copy(tmp, 0, aRespData, tmpResponseData.Length, len);
                            }
                        } while (len > 0);
                    }
                //}

                respnseHeaders = response.Headers;
            }
            catch (System.Net.WebException we)
            {
                if ((HttpWebResponse)we.Response == null)
                {
                    statusCode = HttpStatusCode.InternalServerError;
                }
                else
                {
                    statusCode = ((HttpWebResponse)we.Response).StatusCode;
                    respnseHeaders = ((HttpWebResponse)we.Response).Headers;
                }

                byte[] tmp;
                byte[] tmpResponseData;
                //if (statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.Accepted)
                //{
                response = (HttpWebResponse)we.Response;
                if (response != null)
                {
                    using (BinaryReader br = new BinaryReader(response.GetResponseStream()))
                    {
                        int len;
                        do
                        {
                            if (response.ContentLength > 0)
                                tmp = new byte[response.ContentLength];
                            else
                                tmp = new byte[BUFFER_SIZE];

                            len = br.Read(tmp, 0, tmp.Length);
                            if (aRespData == null)
                            {
                                aRespData = new byte[len];
                                Array.Copy(tmp, aRespData, len);
                            }
                            else
                            {
                                tmpResponseData = aRespData;
                                aRespData = new byte[tmpResponseData.Length + len];
                                Array.Copy(tmpResponseData, aRespData, tmpResponseData.Length);
                                Array.Copy(tmp, 0, aRespData, tmpResponseData.Length, len);
                            }
                        } while (len > 0);
                    }
                }
                
                //}
            }
            catch (System.Exception e)
            {
            	
            }
            finally
            {
                if (respStream != null)
                    respStream.Close();

                if (reqStream != null)
                    reqStream.Close();

                if (response != null)
                    response.Close();

                if (request != null)
                    request.Abort();
            }

            return statusCode;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, 
            X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
    }
}
