using LabMIO.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMIO.Data.Blocks
{
    public class DiffBlock : BaseRememberBlock
    {
        public DiffBlock(double dt) : base(dt)
        {
        }

        public override double CalculateOutput(double input)
        {
            var output = (input - _previous) / _dt;
            _previous = input;
            return output;
        }
    }
}
