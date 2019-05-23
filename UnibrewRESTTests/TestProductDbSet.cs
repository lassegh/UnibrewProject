using System;
using System.Linq;
using UnibrewREST;

namespace UnibrewRESTTests
{
    class TestProductDbSet : FullDBmodelTests<TapOperator>
    {
        public override TapOperator Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.ID == (int)keyValues.Single());
        }
    }
}