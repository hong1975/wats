using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEmiReportTool
{
    public class Link
    {
        public static Link Clone(Link link)
        {
            Link newLink = new Link();
            newLink.LinkID = link.LinkID;
            Channel newChannel;
            foreach (Channel channel in link.Channels)
            {
                newChannel = Channel.Clone(channel);
                newLink.Channels.Add(newChannel);
            }

            return newLink;
        }

        public class Channel
        {
            public static Channel Clone(Channel channel)
            {
                Channel newChannel = new Channel();
                newChannel.ChannelID = channel.ChannelID;
                newChannel.TX = channel.TX;
                newChannel.RX = channel.RX;
                newChannel.Result = channel.Result;
                newChannel.SubBand = channel.SubBand;

                return newChannel;
            }
            public string ChannelID;
            public double TX;
            public double RX;
            public string Result;
            public string SubBand;
        }

        public string LinkID;
        public List<Channel> Channels = new List<Channel>();
        public List<Link> NextLinks = new List<Link>();

        public void Sort()
        {
            Channels.Sort(SortRountine);
        }

        public static int SortRountine(Channel channel1, Channel channel2)
        {
            return channel1.ChannelID.ToUpper().CompareTo(channel2.ChannelID.ToUpper());
        }
    }
}
