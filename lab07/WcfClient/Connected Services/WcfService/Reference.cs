﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfClient.WcfService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WcfService.ICalculator")]
    public interface ICalculator {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculator/iAdd", ReplyAction="http://tempuri.org/ICalculator/iAddResponse")]
        int iAdd(int n1, int n2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculator/iAdd", ReplyAction="http://tempuri.org/ICalculator/iAddResponse")]
        System.Threading.Tasks.Task<int> iAddAsync(int n1, int n2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculator/iSub", ReplyAction="http://tempuri.org/ICalculator/iSubResponse")]
        int iSub(int n1, int n2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculator/iSub", ReplyAction="http://tempuri.org/ICalculator/iSubResponse")]
        System.Threading.Tasks.Task<int> iSubAsync(int n1, int n2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculator/iMul", ReplyAction="http://tempuri.org/ICalculator/iMulResponse")]
        int iMul(int n1, int n2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculator/iMul", ReplyAction="http://tempuri.org/ICalculator/iMulResponse")]
        System.Threading.Tasks.Task<int> iMulAsync(int n1, int n2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculator/iDiv", ReplyAction="http://tempuri.org/ICalculator/iDivResponse")]
        int iDiv(int n1, int n2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculator/iDiv", ReplyAction="http://tempuri.org/ICalculator/iDivResponse")]
        System.Threading.Tasks.Task<int> iDivAsync(int n1, int n2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculator/iMod", ReplyAction="http://tempuri.org/ICalculator/iModResponse")]
        int iMod(int n1, int n2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculator/iMod", ReplyAction="http://tempuri.org/ICalculator/iModResponse")]
        System.Threading.Tasks.Task<int> iModAsync(int n1, int n2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculator/HMult", ReplyAction="http://tempuri.org/ICalculator/HMultResponse")]
        int HMult(int n1, int n2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculator/HMult", ReplyAction="http://tempuri.org/ICalculator/HMultResponse")]
        System.Threading.Tasks.Task<int> HMultAsync(int n1, int n2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculator/CountAndMaxPrimesInRange", ReplyAction="http://tempuri.org/ICalculator/CountAndMaxPrimesInRangeResponse")]
        System.ValueTuple<int, int> CountAndMaxPrimesInRange(int l1, int l2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculator/CountAndMaxPrimesInRange", ReplyAction="http://tempuri.org/ICalculator/CountAndMaxPrimesInRangeResponse")]
        System.Threading.Tasks.Task<System.ValueTuple<int, int>> CountAndMaxPrimesInRangeAsync(int l1, int l2);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICalculatorChannel : WcfClient.WcfService.ICalculator, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CalculatorClient : System.ServiceModel.ClientBase<WcfClient.WcfService.ICalculator>, WcfClient.WcfService.ICalculator {
        
        public CalculatorClient() {
        }
        
        public CalculatorClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CalculatorClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CalculatorClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CalculatorClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int iAdd(int n1, int n2) {
            return base.Channel.iAdd(n1, n2);
        }
        
        public System.Threading.Tasks.Task<int> iAddAsync(int n1, int n2) {
            return base.Channel.iAddAsync(n1, n2);
        }
        
        public int iSub(int n1, int n2) {
            return base.Channel.iSub(n1, n2);
        }
        
        public System.Threading.Tasks.Task<int> iSubAsync(int n1, int n2) {
            return base.Channel.iSubAsync(n1, n2);
        }
        
        public int iMul(int n1, int n2) {
            return base.Channel.iMul(n1, n2);
        }
        
        public System.Threading.Tasks.Task<int> iMulAsync(int n1, int n2) {
            return base.Channel.iMulAsync(n1, n2);
        }
        
        public int iDiv(int n1, int n2) {
            return base.Channel.iDiv(n1, n2);
        }
        
        public System.Threading.Tasks.Task<int> iDivAsync(int n1, int n2) {
            return base.Channel.iDivAsync(n1, n2);
        }
        
        public int iMod(int n1, int n2) {
            return base.Channel.iMod(n1, n2);
        }
        
        public System.Threading.Tasks.Task<int> iModAsync(int n1, int n2) {
            return base.Channel.iModAsync(n1, n2);
        }
        
        public int HMult(int n1, int n2) {
            return base.Channel.HMult(n1, n2);
        }
        
        public System.Threading.Tasks.Task<int> HMultAsync(int n1, int n2) {
            return base.Channel.HMultAsync(n1, n2);
        }
        
        public System.ValueTuple<int, int> CountAndMaxPrimesInRange(int l1, int l2) {
            return base.Channel.CountAndMaxPrimesInRange(l1, l2);
        }
        
        public System.Threading.Tasks.Task<System.ValueTuple<int, int>> CountAndMaxPrimesInRangeAsync(int l1, int l2) {
            return base.Channel.CountAndMaxPrimesInRangeAsync(l1, l2);
        }
    }
}
