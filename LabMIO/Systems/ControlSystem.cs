using LabMIO.Data.Blocks;
using LabMIO.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMIO.Systems
{
    public class ControlSystem
    {
        private ObjectModel _obj;
        private PIDBlock _pid;
        private double _dt;
        public double Time { get; set; }
        public double Z1 { get; set; }
        public double Z2 { get; set; }
        public bool IsAuto { get; set; }

        public ControlSystem(PIDBlock pid, double dt)
        {
            this._dt = dt;
            _obj = new ObjectModel(1, 20, 25, 1, 1, dt);
            _pid = pid;

        }
        public void Calculate(double x1, double x2, double x1_2, double xout1)
        {
            Time += _dt;
            var objX1 = x1;
            if(IsAuto)
            {
                var pidInput = x1 - _obj.Z2;
                objX1 = _pid.CalculateOutput(pidInput);
            }
            
            _obj.Calculate(objX1, x2, x1_2, xout1);
            Z1 = _obj.Z1;
            Z2 = _obj.Z2;
        }

    }
}
