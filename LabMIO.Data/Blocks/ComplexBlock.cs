using LabMIO.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMIO.Data.Blocks
{
    public class ComplexBlock : IBlock
    {
        private Queue<IBlock> Blocks { get; }

        public ComplexBlock(Queue<IBlock> blocks)
        {
            Blocks = blocks;
        }

        public double CalculateOutput(double input)
        {
            foreach(var block in Blocks)
            {
                input = block.CalculateOutput(input);
            }
            return input;
        }
    }
}
