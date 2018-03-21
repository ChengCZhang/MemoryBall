using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace MemoryBall
{
    public class MemoryInfo : INotifyPropertyChanged
    {
        private readonly double[] table;
        private readonly int r, R, offset;

        public MemoryInfo()
        {
            table = new double[26]
            {0, 0.06279052, 0.125333234, 0.187381315, 0.248689887, 0.309016994,
                0.368124553, 0.425779292, 0.481753674, 0.535826795, 0.587785252,
                0.63742399, 0.684547106, 0.728968627, 0.770513243, 0.809016994,
                0.844327926, 0.87630668, 0.904827052, 0.929776486, 0.951056516,
                0.968583161, 0.982287251, 0.992114701, 0.998026728, 1};
            r = 26;
            R = 36;
            offset = 38;
            innerPoint = outerPoint = _inner = _outer = new Point(38, 2);
        }

        #region 属性
        private Point _inner;
        private Point innerPoint;
        public Point InnerPoint
        {
            get { return innerPoint; }
            private set
            {
                if (innerPoint != value)
                {
                    innerPoint = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("InnerPoint"));
                }
            }
        }

        private Point _outer;
        private Point outerPoint;
        public Point OuterPoint
        {
            get { return outerPoint; }
            private set
            {
                if (outerPoint != value)
                {
                    outerPoint = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("OuterPoint"));
                }
            }
        }

        private string memoryLoad;
        public string MemoryLoad
        {
            get { return memoryLoad; }
            private set
            {
                if (memoryLoad != value)
                {
                    memoryLoad = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("MemoryLoad"));
                }
            }
        }

        private bool isLargeArc;
        public bool IsLargeArc
        {
            get { return isLargeArc; }
            set
            {
                if (isLargeArc != value)
                {
                    isLargeArc = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("IsLargeArc"));
                }
            }
        }

        private Brush fillColor;
        public Brush FillColor
        {
            get { return fillColor; }
            set
            {
                if (fillColor != value)
                {
                    fillColor = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("FillColor"));
                }
            }
        }


        private int memLoad;
        public int MemLoad
        {
            get { return memLoad; }
            set
            {
                FillColor = SystemParameters.WindowGlassBrush;
                if (memLoad != value)
                {
                    memLoad = value;
                    UpdateMemoryInfo();
                }
            }
        }
        #endregion

        #region 信息更新函数
        private void UpdateMemoryInfo()
        {
            MemoryLoad = $"{memLoad}%";
            if (memLoad < 25)
            {
                IsLargeArc = false;
                UpdatePointTX(memLoad);
                UpdatePointFY(25 - memLoad);
                goto end;
            }
            if (memLoad < 50)
            {
                IsLargeArc = false;
                UpdatePointTX(50 - memLoad);
                UpdatePointTY(memLoad - 25);
                goto end;
            }
            if (memLoad < 75)
            {
                IsLargeArc = true;
                UpdatePointFX(memLoad - 50);
                UpdatePointTY(75 - memLoad);
                goto end;
            }
            IsLargeArc = true;
            UpdatePointFX(100 - memLoad);
            UpdatePointFY(memLoad - 75);
            end:
            InnerPoint = _inner;
            OuterPoint = _outer;
        }

        private void UpdatePointTX(int x)
        {
            _inner.X = offset + r * table[x];
            _outer.X = offset + R * table[x];
        }
        private void UpdatePointFX(int x)
        {
            _inner.X = offset - r * table[x];
            _outer.X = offset - R * table[x];
        }

        private void UpdatePointTY(int y)
        {
            _inner.Y = offset + r * table[y];
            _outer.Y = offset + R * table[y];
        }
        private void UpdatePointFY(int y)
        {
            _inner.Y = offset - r * table[y];
            _outer.Y = offset - R * table[y];
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
