using LabMIO.Data.Blocks;
using LabMIO.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMIO.Systems
{
    class ControlSystem
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
        #endregion

        #region Ctor
        public ControlSystem(double k, double T1, double T2, double dt)
        {
            K10 = new GainBlock(k);
            K11 = new GainBlock(k);
            K12 = new GainBlock(k);
            K13 = new GainBlock(k);
            K14 = new GainBlock(k);
            K15 = new GainBlock(k);

            AperiodicBlockZ1 = new AperiodicBlock(T1, dt);
            AperiodicBlockZ2 = new AperiodicBlock(T2, dt);
        }
        #endregion

        public double CalculateZ2(double x1, double x2, double x1_2)
        {
            var k10out = K10.CalculateOutput(x1);
            var k11out = K11.CalculateOutput(x2);
            var k12out = K12.CalculateOutput(x1_2);

            var z2input = k10out + k11out - k12out;
            var z2output = AperiodicBlockZ2.CalculateOutput(z2input);
            return z2output;
        }

        public double CalculateZ1(double x1, double x2, double x1_2, double xout1)
        {
            var k13output = K13.CalculateOutput(x1_2);
            var k15output = K15.CalculateOutput(xout1);

            var z2output = CalculateZ2(x1, x2, x1_2);
            var k14output = K14.CalculateOutput(z2output);
            var z1input = k13output + k14output - k15output;
            var z1output = AperiodicBlockZ1.CalculateOutput(z1input);

            return z1input;
        }
    }
}
