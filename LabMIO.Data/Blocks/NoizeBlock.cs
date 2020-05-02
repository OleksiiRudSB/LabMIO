using LabMIO.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMIO.Data.Blocks
{
    public class NoizeBlock : IBlock
    {
        private double _noise;
        private Random _random;

        public NoizeBlock(double noise)
        {
            _noise = noise;
            _random = new Random();
        }

        public double CalculateOutput(double input)
        {
            var percentage = input * _noise / 100;
            return input + percentage * _random.NextDouble();
        }
    }
}
