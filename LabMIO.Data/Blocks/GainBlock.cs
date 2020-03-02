using LabMIO.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMIO.Data.Blocks
{
    public class GainBlock : IBlock
    {
        public double K { get; }
        public GainBlock(double k)
        {
            K = k;
        }
        public double CalculateOutput(double input)
        {
            return K * input;
        }
    }
}
