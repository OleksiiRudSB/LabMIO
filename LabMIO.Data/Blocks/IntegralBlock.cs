﻿using LabMIO.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMIO.Data.Blocks
{
    public class IntegralBlock : BaseRememberBlock
    {
        private double sum = 0;
        public IntegralBlock(double dt) : base(dt)
        {
        }

        public override double CalculateOutput(double input)
        {
            sum += ((input + _previous) * _dt) / 2;
            _previous = input;
            return sum;
        }
    }
}
