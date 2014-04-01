using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEmiReportTool
{
    public class ChannelPower
    {
        private double mHPower = 0;
        private double mHPairPower = 0;
        private double mVPower = 0;
        private double mVPairPower = 0;
        
        private bool mIsValidVPower = true;
        private bool mIsValidVPairPower = true;
        private bool mIsValidHPower = true;
        private bool mIsValidHPairPower = true;

        public double VPower        { get { return mVPower; } }
        public double VPairPower    { get { return mVPairPower; } }
        public double HPower        { get { return mHPower; } }
        public double HPairPower    { get { return mHPairPower; } }

        public bool IsValidVPower       { get { return mIsValidVPower; } }
        public bool IsValidVPairPower   { get { return mIsValidVPairPower; } }
        public bool IsValidHPower       { get { return mIsValidHPower; } }
        public bool IsValidHPairPower   { get { return mIsValidHPairPower; } }

        public ChannelPower(double emiRbw, ChannelSetting channelSetting, 
            LimitSetting limitSetting, WatsEmiData watsEmiData)
        {
            foreach (WatsEmiSample sample in watsEmiData.mHSamples)
                mHPower += Math.Pow(10, sample.mRssi / 10);
            mHPower = mHPower / watsEmiData.mHSamples.Count;
            mHPower = 10 * Math.Log10(channelSetting.BandWidth * 1000 * mHPower / emiRbw);

            foreach (WatsEmiSample sample in watsEmiData.mHPairSamples)
                mHPairPower += Math.Pow(10, sample.mRssi / 10);
            mHPairPower = mHPairPower / watsEmiData.mHPairSamples.Count;
            mHPairPower = 10 * Math.Log10(channelSetting.Pair.BandWidth * 1000 * mHPairPower / emiRbw);

            foreach (WatsEmiSample sample in watsEmiData.mVSamples)
                mVPower += Math.Pow(10, sample.mRssi / 10);
            mVPower = mVPower / watsEmiData.mVSamples.Count;
            mVPower = 10 * Math.Log10(channelSetting.BandWidth * 1000 * mVPower / emiRbw);

            foreach (WatsEmiSample sample in watsEmiData.mVPairSamples)
                mVPairPower += Math.Pow(10, sample.mRssi / 10);
            mVPairPower = mVPairPower / watsEmiData.mVPairSamples.Count;
            mVPairPower = 10 * Math.Log10(channelSetting.Pair.BandWidth * 1000 * mVPairPower / emiRbw);

            if (limitSetting.UseDeltaPowerLimit 
                && Utility.CalculateDeltaPower(watsEmiData.mHSamples) > Math.Abs(limitSetting.DeltaPowerLimit))
                mIsValidHPower = false;
            if (limitSetting.UseDeltaPowerLimit 
                && Utility.CalculateDeltaPower(watsEmiData.mVSamples) > Math.Abs(limitSetting.DeltaPowerLimit))
                mIsValidVPower = false;
            if (limitSetting.UseDeltaPowerLimit 
                && Utility.CalculateDeltaPower(watsEmiData.mHPairSamples) > Math.Abs(limitSetting.DeltaPowerLimit))
                mIsValidHPairPower = false;
            if (limitSetting.UseDeltaPowerLimit 
                && Utility.CalculateDeltaPower(watsEmiData.mVPairSamples) > Math.Abs(limitSetting.DeltaPowerLimit))
                mIsValidVPairPower = false;

            if (mIsValidHPower && limitSetting.UseChannelPowerLimit && mHPower > limitSetting.ChannelPowerLimit)
                mIsValidHPower = false;
            if (mIsValidVPower && limitSetting.UseChannelPowerLimit && mVPower > limitSetting.ChannelPowerLimit)
                mIsValidVPower = false;
            if (mIsValidHPairPower && limitSetting.UseChannelPowerLimit && mHPairPower > limitSetting.ChannelPowerLimit)
                mIsValidHPairPower = false;
            if (mIsValidVPairPower && limitSetting.UseChannelPowerLimit && mVPairPower > limitSetting.ChannelPowerLimit)
                mIsValidVPairPower = false;
        }
    }
}
