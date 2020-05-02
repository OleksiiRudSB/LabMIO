using LabMIO.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMIO.Data.Blocks
{
    public class DelayBlock : IBlock
    {
        private int _bufferSize;
        private Queue<double> _buffer;

        public DelayBlock(double time, double dt)
        {
            _bufferSize = (int)(time / dt);
            _buffer = new Queue<double>();
        }

        public double CalculateOutput(double input)
        {
            _buffer.Enqueue(input);
            if (_buffer.Count <= _bufferSize)
            {
                return 0;
            }
            else
            {
                return _buffer.Dequeue();
            }
        }
    }
}
