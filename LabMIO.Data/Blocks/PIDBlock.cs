using LabMIO.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMIO.Data.Blocks
{
    public class PIDBlock : BaseRememberBlock
    {
        private GainBlock _gain;
        private IntegralBlock _integral;
        private DiffBlock _diff;

        private AperiodicBlock _kDamper;
        private AperiodicBlock _kiDamper;
        private AperiodicBlock _kdDamper;

        public bool IsAuto { get; set; }

        public double K { get; set; }
        public double Ki { get; set; }
        public double Kd { get; set; }

        public double Output { get; set; }

        public PIDBlock(double k, double ki, double kd, double dt) : base(dt)
        {
            K = k;
            Ki = ki;
            Kd = kd;

            _gain = new GainBlock(1);
            _integral = new IntegralBlock(dt);
            _diff = new DiffBlock(dt);

            _kDamper = new AperiodicBlock(30, dt);
            _kiDamper = new AperiodicBlock(30, dt);
            _kdDamper = new AperiodicBlock(30, dt);
        }

        public override double CalculateOutput(double input)
        {
            double kDamperValue = _kDamper.CalculateOutput(K);
            double kiDamperValue = _kiDamper.CalculateOutput(Ki);
            double kdDamperValue = _kdDamper.CalculateOutput(Kd);

            Ki = kiDamperValue;
            Kd = kdDamperValue;

            var result = 0d;
            result += _gain.CalculateOutput(input);
            result += Ki * _diff.CalculateOutput(input);

            var integralOutput = _integral.CalculateOutput(input);

            if (!IsAuto)
            {
                integralOutput = _previous / kDamperValue - result;
            }

            result += integralOutput;

            _previous = input;

            Output = K * result;

            return Output;
        }
    }
}
