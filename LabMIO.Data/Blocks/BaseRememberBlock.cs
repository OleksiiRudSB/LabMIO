using LabMIO.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMIO.Data.Blocks
{
    public abstract class BaseRememberBlock : IBlock
    {
        protected double _dt;
        protected double _previous;

        public BaseRememberBlock(double dt)
        {
            _dt = dt;
        }

        public abstract double CalculateOutput(double input);
    }
}
