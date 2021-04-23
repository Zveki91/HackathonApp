using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System.Numerics;

namespace HackathonApp.Interfaces.Contract
{
    public class H4PToken
    {
        [Function("approve", "bool")]
        public class ApproveFunction : FunctionMessage
        {
            [Parameter("address", "spender", 1)]
            public string Spender { get; set; }
            [Parameter("uint256", "amount", 2)]
            public BigInteger Amount { get; set; }
        }

        [Function("decreaseAllowance", "bool")]
        public class DecreaseAllowanceFunction : FunctionMessage
        {
            [Parameter("address", "spender", 1)]
            public string Spender { get; set; }
            [Parameter("uint256", "subtractedValue", 2)]
            public BigInteger SubtractedValue { get; set; }
        }

        [Function("increaseAllowance", "bool")]
        public class IncreaseAllowanceFunction : FunctionMessage
        {
            [Parameter("address", "spender", 1)]
            public string Spender { get; set; }
            [Parameter("uint256", "addedValue", 2)]
            public BigInteger AddedValue { get; set; }
        }

        [Function("transfer", "bool")]
        public class TransferFunction : FunctionMessage
        {
            [Parameter("address", "recipient", 1)]
            public string Recipient { get; set; }
            [Parameter("uint256", "amount", 2)]
            public BigInteger Amount { get; set; }
        }

        [Function("transferFrom", "bool")]
        public class TransferFromFunction : FunctionMessage
        {
            [Parameter("address", "sender", 1)]
            public string Sender { get; set; }
            [Parameter("address", "recipient", 2)]
            public string Recipient { get; set; }
            [Parameter("uint256", "amount", 3)]
            public BigInteger Amount { get; set; }
        }

        [Function("allowance", "uint256")]
        public class AllowanceFunction : FunctionMessage
        {
            [Parameter("address", "owner", 1)]
            public string Owner { get; set; }
            [Parameter("address", "spender", 2)]
            public string Spender { get; set; }
        }

        [Function("balanceOf", "uint256")]
        public class BalanceOfFunction : FunctionMessage
        {
            [Parameter("address", "account", 1)]
            public string Account { get; set; }
        }

        [Function("decimals", "uint8")]
        public class DecimalsFunction : FunctionMessage { }

        [Function("name", "string")]
        public class NameFunction : FunctionMessage { }

        [Function("symbol", "string")]
        public class SymbolFunction : FunctionMessage { }

        [Function("totalSupply", "uint256")]
        public class TotalSupplyFunction : FunctionMessage { }

        [Event("Approval")]
        public class ApprovalEventDTO : IEventDTO
        {
            [Parameter("address", "owner", 1, true)]
            public string Owner { get; set; }
            [Parameter("address", "spender", 2, true)]
            public string Spender { get; set; }
            [Parameter("uint256", "value", 3, false)]
            public BigInteger Value { get; set; }
        }

        [Event("Transfer")]
        public class TransferEventDTO : IEventDTO
        {
            [Parameter("address", "from", 1, true)]
            public string From { get; set; }
            [Parameter("address", "to", 2, true)]
            public string To { get; set; }
            [Parameter("uint256", "value", 3, false)]
            public BigInteger Value { get; set; }
        }

        [FunctionOutput]
        public class AllowanceOutputDTO : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public BigInteger ReturnValue1 { get; set; }
        }

        [FunctionOutput]
        public class BalanceOfOutputDTO : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public BigInteger ReturnValue1 { get; set; }
        }

        [FunctionOutput]
        public class DecimalsOutputDTO : IFunctionOutputDTO
        {
            [Parameter("uint8", "", 1)]
            public byte ReturnValue1 { get; set; }
        }

        [FunctionOutput]
        public class NameOutputDTO : IFunctionOutputDTO
        {
            [Parameter("string", "", 1)]
            public string ReturnValue1 { get; set; }
        }

        [FunctionOutput]
        public class SymbolOutputDTO : IFunctionOutputDTO
        {
            [Parameter("string", "", 1)]
            public string ReturnValue1 { get; set; }
        }

        [FunctionOutput]
        public class TotalSupplyOutputDTO : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public BigInteger ReturnValue1 { get; set; }
        }
    }
}
