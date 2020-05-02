using LabMIO.Data.Blocks;
using LabMIO.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMIO.Objects
{
    public class ObjectModel
    {
        #region Blocks
        private GainBlock K10;
        private GainBlock K11;
        private GainBlock K12;
        private GainBlock K13;
        private GainBlock K14;
        private GainBlock K15;

        private AperiodicBlock AperiodicBlockZ1;
        private AperiodicBlock AperiodicBlockZ2;

        private DelayBlock Z2Delay;
        private NoizeBlock Z2Noize;
        private DelayBlock Z1Delay;
        private NoizeBlock Z1Noize;
        #endregion

        public double Z1 { get; set; }
        public double Z2 { get; set; }

        #region Ctor
        public ObjectModel(double k, double T1, double T2, double delayTime, double noize, double dt)
        {
            K10 = new GainBlock(k);
            K11 = new GainBlock(k);
            K12 = new GainBlock(k);
            K13 = new GainBlock(k);
            K14 = new GainBlock(k);
            K15 = new GainBlock(k);

            AperiodicBlockZ1 = new AperiodicBlock(T1, dt);
            AperiodicBlockZ2 = new AperiodicBlock(T2, dt);

            Z2Delay = new DelayBlock(delayTime, dt);
            Z2Noize = new NoizeBlock(noize);
            Z1Delay = new DelayBlock(delayTime, dt);
            Z1Noize = new NoizeBlock(noize);
        }
        #endregion

        private double CalculateZ2(double x1, double x2, double x1_2)
        {
            var k10out = K10.CalculateOutput(x1);
            var k11out = K11.CalculateOutput(x2);
            var k12out = K12.CalculateOutput(x1_2);

            var z2input = k10out + k11out - k12out;
            var z2output = AperiodicBlockZ2.CalculateOutput(z2input);
            var z2DelayOutput = Z2Delay.CalculateOutput(z2output);
            var z2NoizeOutput = Z2Noize.CalculateOutput(z2DelayOutput);
            return z2NoizeOutput;
        }

        public void Calculate(double x1, double x2, double x1_2, double xout1)
        {
            var k13output = K13.CalculateOutput(x1_2);
            var k15output = K15.CalculateOutput(xout1);

            Z2 = CalculateZ2(x1, x2, x1_2);
            var k14output = K14.CalculateOutput(Z2);
            var z1input = k13output + k14output - k15output;
            var z1output = AperiodicBlockZ1.CalculateOutput(z1input);
            var z1DelayOutput = Z1Delay.CalculateOutput(z1output);
            Z1 = Z1Noize.CalculateOutput(z1DelayOutput);
        }
    }
}
