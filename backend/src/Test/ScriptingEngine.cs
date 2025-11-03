using System.Linq.Expressions;

namespace Test
{
    public class ScriptingEngine
    {
        public static Expression IsPrime(ParameterExpression value)
        {
            var label = Expression.Label();
            var result = Expression.Parameter(typeof(bool), "result");
            var returnLabel = Expression.Label(typeof(bool));

            var valueLessThanEqualToOne = Expression.LessThanOrEqual(value, Expression.Constant(1));
            var valueEqualTwo = Expression.Equal(value, Expression.Constant(2));
            var valueModTwoZero = Expression.Equal(Expression.Modulo(value, Expression.Constant(2)), Expression.Constant(0));

            var sqRt = typeof(Math).GetMethod("Sqrt");
            var floor = typeof(Math).GetMethod("Floor", [typeof(double)]);

            var valueSqRt = Expression.Call(null, sqRt, Expression.Convert(value, typeof(double)));

            var evalFunction = Expression.Convert(Expression.Call(null, floor, valueSqRt), typeof(int));
            
            var boundary = Expression.Variable(typeof(int), "boundary");
            var i = Expression.Variable(typeof(int), "i");

            var modBlock = Expression.Block(
                [i, boundary],
                Expression.IfThen(
                    Expression.Equal(Expression.Modulo(value, i), Expression.Constant(0)),
                    Expression.Return(returnLabel, Expression.Constant(false))
                ),
                Expression.Assign(i, Expression.Constant(2))
            );

            var block = Expression.Block(
                [result, i, boundary],
                Expression.IfThen(valueLessThanEqualToOne,
                    Expression.Return(returnLabel, Expression.Constant(false))
                ),
                Expression.IfThen(valueEqualTwo,
                    Expression.Return(returnLabel, Expression.Constant(true))
                ),
                Expression.IfThen(valueModTwoZero,
                    Expression.Return(returnLabel, Expression.Constant(false))
                ),
                Expression.Assign(i, Expression.Constant(3)),
                Expression.Assign(boundary, evalFunction),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThanOrEqual(i, boundary),
                        modBlock,
                        Expression.Break(label)
                    ),
                    label
                ),
                Expression.Return(returnLabel, Expression.Constant(true)),
                Expression.Label(returnLabel, Expression.Constant(false))
            );

            return block;
        }
    }
}