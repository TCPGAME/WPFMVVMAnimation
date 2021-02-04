using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFMVVMAnimation
{
    /// <summary>
    /// UCBAR.xaml 的交互逻辑
    /// </summary>
    public partial class UCBAR : UserControl
    {
        //界面的属性名
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
                "Text", typeof(string), typeof(UCBAR), new PropertyMetadata("属性名"));
        public double Text
        {
            get { return (double)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        //界面矩形的宽
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
                "Value", typeof(double), typeof(UCBAR), new PropertyMetadata(30d));
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private double lastValue;
        DoubleAnimation doubleAnimation;
        public UCBAR()
        {
            InitializeComponent();

            //实例化DoubleAnimation
            doubleAnimation = new DoubleAnimation()
            {
                Duration = new TimeSpan(0, 0, 0, 0, 300),
            };
            //动画结束后，保持最后的值
            doubleAnimation.FillBehavior = FillBehavior.HoldEnd;
            //动画结束后的事件
            doubleAnimation.Completed += DoubleAnimation_Completed;

            //重点  创建一个属性监器，并监听ValueProperty改变，如果ValueProperty改变了将触发SelfChanges方法
            var descriptor = System.ComponentModel.DependencyPropertyDescriptor.FromProperty(UCBAR.ValueProperty, typeof(UCBAR));
            descriptor.AddValueChanged(this, SelfChanges);
        }

        private void SelfChanges(object sender, EventArgs e)
        {
            doubleAnimation.From = lastValue;
            doubleAnimation.To = Value;
            RectView.BeginAnimation(WidthProperty, doubleAnimation);
        }
        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            lastValue = (double)doubleAnimation.To;
        }

    }
}
