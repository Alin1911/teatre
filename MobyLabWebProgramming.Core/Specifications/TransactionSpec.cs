using MobyLabWebProgramming.Core.Entities;


namespace MobyLabWebProgramming.Core.Specifications;


public sealed class TransactionSpec : BaseSpec<TransactionSpec, Transaction>
    {
        public TransactionSpec(Guid id) : base(id)
        {
        }
    }
