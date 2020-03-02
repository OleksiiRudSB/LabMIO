using LabMIO.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMIO.Data.Blocks
{
    public class AperiodicBlock :  BaseRememberBlock
    {
        private double T { get; }
        public AperiodicBlock(double T, double dt) : base(dt)
        {
            this.T = T;
        }

        public override double CalculateOutput(double input)
        {
            var output = (_dt * input + T * _previous) / (T + _dt);
            _previous = output;
            return output;
        }
    }
}
