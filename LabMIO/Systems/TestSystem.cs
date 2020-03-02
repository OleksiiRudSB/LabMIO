using LabMIO.Data.Blocks;
using LabMIO.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMIO.Systems
{
    public interface ISystem
    {
        double Next(double input);
    }

    class TestSystem : ISystem
    {
        private ComplexBlock Block { get; }

        public TestSystem()
        {
            var blocks = new Queue<IBlock>();
            blocks.Enqueue(new GainBlock(4));
            blocks.Enqueue(new AperiodicBlock(50, 1));
            blocks.Enqueue(new AperiodicBlock(25, 1));
            Block = new ComplexBlock(blocks);
        }

        public double Next(double input)
        {
            var result = Block.CalculateOutput(input);
            return result;
        }
    }
}
