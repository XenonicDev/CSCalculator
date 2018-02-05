﻿using System;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using CSCalculator.Core;

namespace CSCalculatorGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ExpressionBuilder Builder;

        public MainWindow()
        {
            InitializeComponent();

            Builder = new ExpressionBuilder();
        }

        private void UpdateExpression()
        {
            ResultBox.Content = Builder.GetExpression();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Numpad_7_Click(object sender, RoutedEventArgs e)
        {
            Builder.Add('7');

            UpdateExpression();
        }

        private void Numpad_9_Click(object sender, RoutedEventArgs e)
        {
            Builder.Add('9');

            UpdateExpression();
        }

        private void Numpad_4_Click(object sender, RoutedEventArgs e)
        {
            Builder.Add('4');

            UpdateExpression();
        }

        private void Numpad_5_Click(object sender, RoutedEventArgs e)
        {
            Builder.Add('5');

            UpdateExpression();
        }

        private void Numpad_6_Click(object sender, RoutedEventArgs e)
        {
            Builder.Add('6');

            UpdateExpression();
        }

        private void Numpad_1_Click(object sender, RoutedEventArgs e)
        {
            Builder.Add('1');

            UpdateExpression();
        }

        private void Numpad_2_Click(object sender, RoutedEventArgs e)
        {
            Builder.Add('2');

            UpdateExpression();
        }

        private void Numpad_3_Click(object sender, RoutedEventArgs e)
        {
            Builder.Add('3');

            UpdateExpression();
        }

        private void Numpad_0_Click(object sender, RoutedEventArgs e)
        {
            Builder.Add('0');

            UpdateExpression();
        }

        private void Numpad_Decimal_Click(object sender, RoutedEventArgs e)
        {
            Builder.Add('.');

            UpdateExpression();
        }

        private void Numpad_Exponent_Click(object sender, RoutedEventArgs e)
        {
            Builder.Add('^');

            UpdateExpression();
        }

        private void Operation_Negate_Click(object sender, RoutedEventArgs e)
        {
            Builder.Add('-');

            UpdateExpression();
        }

        private void Command_AllClear_Click(object sender, RoutedEventArgs e)
        {
            Builder.Clear();

            UpdateExpression();
        }

        private void Operation_Divide_Click(object sender, RoutedEventArgs e)
        {
            Builder.Add('/');

            UpdateExpression();
        }

        private void Operation_Subtract_Click(object sender, RoutedEventArgs e)
        {
            Builder.Add('-');

            UpdateExpression();
        }

        private void Command_Execute_Click(object sender, RoutedEventArgs e)
        {
            string Result = CSCalculator.Core.Application.Solve(Builder.GetExpression()).ToString();
            ResultBox.Content = Result;

            Memory.History.Add(new CExpression(Builder.GetExpression(), Result));
        }

        private void Command_Delete_Click(object sender, RoutedEventArgs e)
        {
            Builder.RemoveLast();

            UpdateExpression();
        }

        private void Operation_Multiply_Click(object sender, RoutedEventArgs e)
        {
            Builder.Add('*');

            UpdateExpression();
        }

        private void Operation_Add_Click(object sender, RoutedEventArgs e)
        {
            Builder.Add('+');

            UpdateExpression();
        }
    }
}
