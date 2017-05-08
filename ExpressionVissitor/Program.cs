using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static int Eval(IExpresion ex)
        {
            return ex.Visit<int>(
                    onAdd : add => Eval(add.A) + Eval(add.B),
                    onDiv : div => Eval(div.A) / Eval(div.B),
                    onMul : mul => Eval(mul.A) * Eval(mul.B),
                    onSub : sub => Eval(sub.A) - Eval(sub.B),
                    onNumber: num => num.X
                );
        }

        static int Eval2(IExpresion ex)
        {
            switch (ex)
            {
                case Add add:
                    return Eval2(add.A) + Eval2(add.B);
                case Div div:
                    return Eval2(div.A) / Eval2(div.B);
                case Mul mul:
                    return Eval2(mul.A) * Eval2(mul.B);
                case Sub sub:
                    return Eval2(sub.A) - Eval2(sub.B);
                case NumberEx num:
                    return num.X;
                default:
                    throw new NotSupportedException();
            }
        }
    }


    public interface IExpresion
    {
        T Visit<T>(Func<Add,T> onAdd, Func<Sub,T> onSub, Func<Mul,T> onMul, Func<Div,T> onDiv, Func<NumberEx, T> onNumber);
        int Eval();
    }

    public class Div : IExpresion
    {
        public IExpresion A { get; set; }
        public IExpresion B { get; set; }

        public int Eval()
        {
            return A.Eval() / B.Eval();
        }

        public T Visit<T>(Func<Add, T> onAdd, Func<Sub, T> onSub, Func<Mul, T> onMul, Func<Div, T> onDiv, Func<NumberEx, T> onNumber)
        {
            return onDiv(this);
        }
    }

    public class NumberEx : IExpresion
    {
        public int X { get; set; }

        public int Eval()
        {
            return X;
        }

        public T Visit<T>(Func<Add, T> onAdd, Func<Sub, T> onSub, Func<Mul, T> onMul, Func<Div, T> onDiv, Func<NumberEx, T> onNumber)
        {
            return onNumber(this);
        }
    }

    public class Mul : IExpresion
    {
        public IExpresion A { get; set; }
        public IExpresion B { get; set; }

        public int Eval()
        {
            return A.Eval() * B.Eval();
        }

        public T Visit<T>(Func<Add, T> onAdd, Func<Sub, T> onSub, Func<Mul, T> onMul, Func<Div, T> onDiv, Func<NumberEx, T> onNumber)
        {
            return onMul(this);
        }
    }

    public class Sub : IExpresion
    {
        public IExpresion A { get; set; }
        public IExpresion B { get; set; }

        public int Eval()
        {
            return A.Eval() - B.Eval();
        }

        public T Visit<T>(Func<Add, T> onAdd, Func<Sub, T> onSub, Func<Mul, T> onMul, Func<Div, T> onDiv, Func<NumberEx, T> onNumber)
        {
            return onSub(this);
        }
    }

    public class Add : IExpresion
    {
        public IExpresion A { get; set; }
        public IExpresion B { get; set; }

        public int Eval()
        {
            return A.Eval() + B.Eval();
        }

        public T Visit<T>(Func<Add, T> onAdd, Func<Sub, T> onSub, Func<Mul, T> onMul, Func<Div, T> onDiv, Func<NumberEx, T> onNumber)
        {
            return onAdd(this);
        }
    }
}
