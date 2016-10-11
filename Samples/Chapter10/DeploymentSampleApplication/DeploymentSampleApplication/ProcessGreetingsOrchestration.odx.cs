
#pragma warning disable 162

namespace DeploymentSampleApplication
{

    [Microsoft.XLANGs.BaseTypes.PortTypeOperationAttribute(
        "RcvGreetingsOperation",
        new System.Type[]{
            typeof(DeploymentSampleApplication.__messagetype_DeploymentSampleApplication_GreetingsSchema)
        },
        new string[]{
        }
    )]
    [Microsoft.XLANGs.BaseTypes.PortTypeAttribute(Microsoft.XLANGs.BaseTypes.EXLangSAccess.eInternal, "")]
    [System.SerializableAttribute]
    sealed internal class ReceiveGreetingsPortType : Microsoft.BizTalk.XLANGs.BTXEngine.BTXPortBase
    {
        public ReceiveGreetingsPortType(int portInfo, Microsoft.XLANGs.Core.IServiceProxy s)
            : base(portInfo, s)
        { }
        public ReceiveGreetingsPortType(ReceiveGreetingsPortType p)
            : base(p)
        { }

        public override Microsoft.XLANGs.Core.PortBase Clone()
        {
            ReceiveGreetingsPortType p = new ReceiveGreetingsPortType(this);
            return p;
        }

        public static readonly Microsoft.XLANGs.BaseTypes.EXLangSAccess __access = Microsoft.XLANGs.BaseTypes.EXLangSAccess.eInternal;
        #region port reflection support
        static public Microsoft.XLANGs.Core.OperationInfo RcvGreetingsOperation = new Microsoft.XLANGs.Core.OperationInfo
        (
            "RcvGreetingsOperation",
            System.Web.Services.Description.OperationFlow.OneWay,
            typeof(ReceiveGreetingsPortType),
            typeof(__messagetype_DeploymentSampleApplication_GreetingsSchema),
            null,
            new System.Type[]{},
            new string[]{}
        );
        static public System.Collections.Hashtable OperationsInformation
        {
            get
            {
                System.Collections.Hashtable h = new System.Collections.Hashtable();
                h[ "RcvGreetingsOperation" ] = RcvGreetingsOperation;
                return h;
            }
        }
        #endregion // port reflection support
    }

    [Microsoft.XLANGs.BaseTypes.PortTypeOperationAttribute(
        "SendGreetingsResponseOperation",
        new System.Type[]{
            typeof(DeploymentSampleApplication.__messagetype_DeploymentSampleApplication_GreetingsResponseSchema)
        },
        new string[]{
        }
    )]
    [Microsoft.XLANGs.BaseTypes.PortTypeAttribute(Microsoft.XLANGs.BaseTypes.EXLangSAccess.eInternal, "")]
    [System.SerializableAttribute]
    sealed internal class SendGreetingsResponsePortType : Microsoft.BizTalk.XLANGs.BTXEngine.BTXPortBase
    {
        public SendGreetingsResponsePortType(int portInfo, Microsoft.XLANGs.Core.IServiceProxy s)
            : base(portInfo, s)
        { }
        public SendGreetingsResponsePortType(SendGreetingsResponsePortType p)
            : base(p)
        { }

        public override Microsoft.XLANGs.Core.PortBase Clone()
        {
            SendGreetingsResponsePortType p = new SendGreetingsResponsePortType(this);
            return p;
        }

        public static readonly Microsoft.XLANGs.BaseTypes.EXLangSAccess __access = Microsoft.XLANGs.BaseTypes.EXLangSAccess.eInternal;
        #region port reflection support
        static public Microsoft.XLANGs.Core.OperationInfo SendGreetingsResponseOperation = new Microsoft.XLANGs.Core.OperationInfo
        (
            "SendGreetingsResponseOperation",
            System.Web.Services.Description.OperationFlow.OneWay,
            typeof(SendGreetingsResponsePortType),
            typeof(__messagetype_DeploymentSampleApplication_GreetingsResponseSchema),
            null,
            new System.Type[]{},
            new string[]{}
        );
        static public System.Collections.Hashtable OperationsInformation
        {
            get
            {
                System.Collections.Hashtable h = new System.Collections.Hashtable();
                h[ "SendGreetingsResponseOperation" ] = SendGreetingsResponseOperation;
                return h;
            }
        }
        #endregion // port reflection support
    }
    //#line 172 "C:\PROBIZTALK\Chapter10\DeploymentSampleApplication\DeploymentSampleApplication\DeploymentSampleApplication\ProcessGreetingsOrchestration.odx"
    [Microsoft.XLANGs.BaseTypes.StaticSubscriptionAttribute(
        0, "ReceiveGreetingsPort", "RcvGreetingsOperation", -1, -1, true
    )]
    [Microsoft.XLANGs.BaseTypes.ServicePortsAttribute(
        new Microsoft.XLANGs.BaseTypes.EXLangSParameter[] {
            Microsoft.XLANGs.BaseTypes.EXLangSParameter.ePort|Microsoft.XLANGs.BaseTypes.EXLangSParameter.eImplements,
            Microsoft.XLANGs.BaseTypes.EXLangSParameter.ePort|Microsoft.XLANGs.BaseTypes.EXLangSParameter.eUses
        },
        new System.Type[] {
            typeof(DeploymentSampleApplication.ReceiveGreetingsPortType),
            typeof(DeploymentSampleApplication.SendGreetingsResponsePortType)
        },
        new System.String[] {
            "ReceiveGreetingsPort",
            "SendGreetingsResponsePort"
        },
        new System.Type[] {
            null,
            null
        }
    )]
    [Microsoft.XLANGs.BaseTypes.ServiceCallTreeAttribute(
        new System.Type[] {
        },
        new System.Type[] {
        },
        new System.Type[] {
        }
    )]
    [Microsoft.XLANGs.BaseTypes.ServiceAttribute(
        Microsoft.XLANGs.BaseTypes.EXLangSAccess.eInternal,
        Microsoft.XLANGs.BaseTypes.EXLangSServiceInfo.eNone
    )]
    [System.SerializableAttribute]
    [Microsoft.XLANGs.BaseTypes.BPELExportableAttribute(false)]
    sealed internal class ProcessGreetingsOrchestration : Microsoft.BizTalk.XLANGs.BTXEngine.BTXService
    {
        public static readonly Microsoft.XLANGs.BaseTypes.EXLangSAccess __access = Microsoft.XLANGs.BaseTypes.EXLangSAccess.eInternal;
        public static readonly bool __execable = false;
        [Microsoft.XLANGs.BaseTypes.CallCompensationAttribute(
            Microsoft.XLANGs.BaseTypes.EXLangSCallCompensationInfo.eNone,
            new System.String[] {
            },
            new System.String[] {
            }
        )]
        public static void __bodyProxy()
        {
        }
        private static System.Guid _serviceId = Microsoft.XLANGs.Core.HashHelper.HashServiceType(typeof(ProcessGreetingsOrchestration));
        private static volatile System.Guid[] _activationSubIds;

        private static new object _lockIdentity = new object();

        public static System.Guid UUID { get { return _serviceId; } }
        public override System.Guid ServiceId { get { return UUID; } }

        protected override System.Guid[] ActivationSubGuids
        {
            get { return _activationSubIds; }
            set { _activationSubIds = value; }
        }

        protected override object StaleStateLock
        {
            get { return _lockIdentity; }
        }

        protected override bool HasActivation { get { return true; } }

        internal bool IsExeced = false;

        static ProcessGreetingsOrchestration()
        {
            Microsoft.BizTalk.XLANGs.BTXEngine.BTXService.CacheStaticState( _serviceId );
        }

        private void ConstructorHelper()
        {
            _segments = new Microsoft.XLANGs.Core.Segment[] {
                new Microsoft.XLANGs.Core.Segment( new Microsoft.XLANGs.Core.Segment.SegmentCode(this.segment0), 0, 0, 0),
                new Microsoft.XLANGs.Core.Segment( new Microsoft.XLANGs.Core.Segment.SegmentCode(this.segment1), 1, 1, 1)
            };

            _Locks = 0;
            _rootContext = new __ProcessGreetingsOrchestration_root_0(this);
            _stateMgrs = new Microsoft.XLANGs.Core.IStateManager[2];
            _stateMgrs[0] = _rootContext;
            FinalConstruct();
        }

        public ProcessGreetingsOrchestration(System.Guid instanceId, Microsoft.BizTalk.XLANGs.BTXEngine.BTXSession session, Microsoft.BizTalk.XLANGs.BTXEngine.BTXEvents tracker)
            : base(instanceId, session, "ProcessGreetingsOrchestration", tracker)
        {
            ConstructorHelper();
        }

        public ProcessGreetingsOrchestration(int callIndex, System.Guid instanceId, Microsoft.BizTalk.XLANGs.BTXEngine.BTXService parent)
            : base(callIndex, instanceId, parent, "ProcessGreetingsOrchestration")
        {
            ConstructorHelper();
        }

        private const string _symInfo = @"
<XsymFile>
<ProcessFlow xmlns:om='http://schemas.microsoft.com/BizTalk/2003/DesignerData'>      <shapeType>RootShape</shapeType>      <ShapeID>9384ecff-3922-4f09-971a-bf53a2e3fc89</ShapeID>      
<children>                          
<ShapeInfo>      <shapeType>ReceiveShape</shapeType>      <ShapeID>6df23575-3958-455e-8c01-4887d78d3bb9</ShapeID>      <ParentLink>ServiceBody_Statement</ParentLink>                <shapeText>Rcv Greetings</shapeText>                  
<children>                </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>ConstructShape</shapeType>      <ShapeID>9481cdef-35eb-4f15-bfad-f0f322586dcb</ShapeID>      <ParentLink>ServiceBody_Statement</ParentLink>                <shapeText>ConstructMessage_GreetingsResponse</shapeText>                  
<children>                          
<ShapeInfo>      <shapeType>TransformShape</shapeType>      <ShapeID>d2504f0f-cb6c-489b-9792-b97220442f17</ShapeID>      <ParentLink>ComplexStatement_Statement</ParentLink>                <shapeText>Transform_GreetingsToGreeetingsResponse</shapeText>                  
<children>                          
<ShapeInfo>      <shapeType>MessagePartRefShape</shapeType>      <ShapeID>33c2b083-4a8d-4d13-9a64-7d4a295df2c6</ShapeID>      <ParentLink>Transform_InputMessagePartRef</ParentLink>                <shapeText>MessagePartReference_1</shapeText>                  
<children>                </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>MessagePartRefShape</shapeType>      <ShapeID>0f0fab43-bbfc-47c3-bf92-e3057d07dbd8</ShapeID>      <ParentLink>Transform_OutputMessagePartRef</ParentLink>                <shapeText>MessagePartReference_2</shapeText>                  
<children>                </children>
  </ShapeInfo>
                  </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>MessageRefShape</shapeType>      <ShapeID>a688f672-8a43-4659-aba9-515e9d84361c</ShapeID>      <ParentLink>Construct_MessageRef</ParentLink>                  
<children>                </children>
  </ShapeInfo>
                  </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>SendShape</shapeType>      <ShapeID>1944e460-8b7c-4d30-86e7-bf6a33c435b1</ShapeID>      <ParentLink>ServiceBody_Statement</ParentLink>                <shapeText>Snd Greetings</shapeText>                  
<children>                </children>
  </ShapeInfo>
                  </children>
  </ProcessFlow>
<Metadata>

<TrkMetadata>
<ActionName>'ProcessGreetingsOrchestration'</ActionName><IsAtomic>0</IsAtomic><Line>172</Line><Position>14</Position><ShapeID>'e211a116-cb8b-44e7-a052-0de295aa0001'</ShapeID>
</TrkMetadata>

<TrkMetadata>
<Line>183</Line><Position>22</Position><ShapeID>'6df23575-3958-455e-8c01-4887d78d3bb9'</ShapeID>
<Messages>
	<MsgInfo><name>GreetingsMsg</name><part>part</part><schema>DeploymentSampleApplication.GreetingsSchema</schema><direction>Out</direction></MsgInfo>
</Messages>
</TrkMetadata>

<TrkMetadata>
<Line>185</Line><Position>13</Position><ShapeID>'9481cdef-35eb-4f15-bfad-f0f322586dcb'</ShapeID>
<Messages>
	<MsgInfo><name>GreetingsResponseMsg</name><part>part</part><schema>DeploymentSampleApplication.GreetingsResponseSchema</schema><direction>Out</direction></MsgInfo>
	<MsgInfo><name>GreetingsMsg</name><part>part</part><schema>DeploymentSampleApplication.GreetingsSchema</schema><direction>In</direction></MsgInfo>
</Messages>
</TrkMetadata>

<TrkMetadata>
<Line>191</Line><Position>13</Position><ShapeID>'1944e460-8b7c-4d30-86e7-bf6a33c435b1'</ShapeID>
<Messages>
	<MsgInfo><name>GreetingsResponseMsg</name><part>part</part><schema>DeploymentSampleApplication.GreetingsResponseSchema</schema><direction>Out</direction></MsgInfo>
</Messages>
</TrkMetadata>
</Metadata>
</XsymFile>";

        public override string odXml { get { return _symODXML; } }

        private const string _symODXML = @"
<?xml version='1.0' encoding='utf-8' standalone='yes'?>
<om:MetaModel MajorVersion='1' MinorVersion='3' Core='2b131234-7959-458d-834f-2dc0769ce683' ScheduleModel='66366196-361d-448d-976f-cab5e87496d2' xmlns:om='http://schemas.microsoft.com/BizTalk/2003/DesignerData'>
    <om:Element Type='Module' OID='a40df819-3ffe-4b8e-b8f1-3681e45dd7ca' LowerBound='1.1' HigherBound='42.1'>
        <om:Property Name='ReportToAnalyst' Value='True' />
        <om:Property Name='Name' Value='DeploymentSampleApplication' />
        <om:Property Name='Signal' Value='False' />
        <om:Element Type='PortType' OID='37b55a8b-c942-42b4-aa7e-7cb3b869b075' ParentLink='Module_PortType' LowerBound='4.1' HigherBound='11.1'>
            <om:Property Name='Synchronous' Value='False' />
            <om:Property Name='TypeModifier' Value='Internal' />
            <om:Property Name='ReportToAnalyst' Value='True' />
            <om:Property Name='Name' Value='ReceiveGreetingsPortType' />
            <om:Property Name='Signal' Value='False' />
            <om:Element Type='OperationDeclaration' OID='35290c6f-371a-493b-8082-17bc83475348' ParentLink='PortType_OperationDeclaration' LowerBound='6.1' HigherBound='10.1'>
                <om:Property Name='OperationType' Value='OneWay' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='Name' Value='RcvGreetingsOperation' />
                <om:Property Name='Signal' Value='True' />
                <om:Element Type='MessageRef' OID='d9ee72d1-d23d-4afd-985c-4ae1a7fbc803' ParentLink='OperationDeclaration_RequestMessageRef' LowerBound='8.13' HigherBound='8.28'>
                    <om:Property Name='Ref' Value='DeploymentSampleApplication.GreetingsSchema' />
                    <om:Property Name='ReportToAnalyst' Value='True' />
                    <om:Property Name='Name' Value='Request' />
                    <om:Property Name='Signal' Value='False' />
                </om:Element>
            </om:Element>
        </om:Element>
        <om:Element Type='PortType' OID='9aefeaad-bf00-460c-a138-a7fdc09d7cde' ParentLink='Module_PortType' LowerBound='11.1' HigherBound='18.1'>
            <om:Property Name='Synchronous' Value='False' />
            <om:Property Name='TypeModifier' Value='Internal' />
            <om:Property Name='ReportToAnalyst' Value='True' />
            <om:Property Name='Name' Value='SendGreetingsResponsePortType' />
            <om:Property Name='Signal' Value='False' />
            <om:Element Type='OperationDeclaration' OID='421a4fac-b209-42f2-84c4-5d9dabf25db2' ParentLink='PortType_OperationDeclaration' LowerBound='13.1' HigherBound='17.1'>
                <om:Property Name='OperationType' Value='OneWay' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='Name' Value='SendGreetingsResponseOperation' />
                <om:Property Name='Signal' Value='True' />
                <om:Element Type='MessageRef' OID='6f0d51ee-f2a9-4266-a8dc-0807bd3a0f01' ParentLink='OperationDeclaration_RequestMessageRef' LowerBound='15.13' HigherBound='15.36'>
                    <om:Property Name='Ref' Value='DeploymentSampleApplication.GreetingsResponseSchema' />
                    <om:Property Name='ReportToAnalyst' Value='True' />
                    <om:Property Name='Name' Value='Request' />
                    <om:Property Name='Signal' Value='False' />
                </om:Element>
            </om:Element>
        </om:Element>
        <om:Element Type='ServiceDeclaration' OID='f3eed6c8-baf3-4489-b92a-22509dc0d24e' ParentLink='Module_ServiceDeclaration' LowerBound='18.1' HigherBound='41.1'>
            <om:Property Name='InitializedTransactionType' Value='False' />
            <om:Property Name='IsInvokable' Value='False' />
            <om:Property Name='TypeModifier' Value='Internal' />
            <om:Property Name='ReportToAnalyst' Value='True' />
            <om:Property Name='Name' Value='ProcessGreetingsOrchestration' />
            <om:Property Name='Signal' Value='True' />
            <om:Element Type='ServiceBody' OID='9384ecff-3922-4f09-971a-bf53a2e3fc89' ParentLink='ServiceDeclaration_ServiceBody'>
                <om:Property Name='Signal' Value='False' />
                <om:Element Type='Receive' OID='6df23575-3958-455e-8c01-4887d78d3bb9' ParentLink='ServiceBody_Statement' LowerBound='29.1' HigherBound='31.1'>
                    <om:Property Name='Activate' Value='True' />
                    <om:Property Name='PortName' Value='ReceiveGreetingsPort' />
                    <om:Property Name='MessageName' Value='GreetingsMsg' />
                    <om:Property Name='OperationName' Value='RcvGreetingsOperation' />
                    <om:Property Name='OperationMessageName' Value='Request' />
                    <om:Property Name='ReportToAnalyst' Value='True' />
                    <om:Property Name='Name' Value='Rcv Greetings' />
                    <om:Property Name='Signal' Value='True' />
                </om:Element>
                <om:Element Type='Construct' OID='9481cdef-35eb-4f15-bfad-f0f322586dcb' ParentLink='ServiceBody_Statement' LowerBound='31.1' HigherBound='37.1'>
                    <om:Property Name='ReportToAnalyst' Value='True' />
                    <om:Property Name='Name' Value='ConstructMessage_GreetingsResponse' />
                    <om:Property Name='Signal' Value='True' />
                    <om:Element Type='Transform' OID='d2504f0f-cb6c-489b-9792-b97220442f17' ParentLink='ComplexStatement_Statement' LowerBound='34.1' HigherBound='36.1'>
                        <om:Property Name='ClassName' Value='DeploymentSampleApplication.GreetingsToGreetingsResponseMap' />
                        <om:Property Name='ReportToAnalyst' Value='True' />
                        <om:Property Name='Name' Value='Transform_GreetingsToGreeetingsResponse' />
                        <om:Property Name='Signal' Value='True' />
                        <om:Element Type='MessagePartRef' OID='33c2b083-4a8d-4d13-9a64-7d4a295df2c6' ParentLink='Transform_InputMessagePartRef' LowerBound='35.113' HigherBound='35.125'>
                            <om:Property Name='MessageRef' Value='GreetingsMsg' />
                            <om:Property Name='ReportToAnalyst' Value='True' />
                            <om:Property Name='Name' Value='MessagePartReference_1' />
                            <om:Property Name='Signal' Value='False' />
                        </om:Element>
                        <om:Element Type='MessagePartRef' OID='0f0fab43-bbfc-47c3-bf92-e3057d07dbd8' ParentLink='Transform_OutputMessagePartRef' LowerBound='35.28' HigherBound='35.48'>
                            <om:Property Name='MessageRef' Value='GreetingsResponseMsg' />
                            <om:Property Name='ReportToAnalyst' Value='True' />
                            <om:Property Name='Name' Value='MessagePartReference_2' />
                            <om:Property Name='Signal' Value='False' />
                        </om:Element>
                    </om:Element>
                    <om:Element Type='MessageRef' OID='a688f672-8a43-4659-aba9-515e9d84361c' ParentLink='Construct_MessageRef' LowerBound='32.23' HigherBound='32.43'>
                        <om:Property Name='Ref' Value='GreetingsResponseMsg' />
                        <om:Property Name='ReportToAnalyst' Value='True' />
                        <om:Property Name='Signal' Value='False' />
                    </om:Element>
                </om:Element>
                <om:Element Type='Send' OID='1944e460-8b7c-4d30-86e7-bf6a33c435b1' ParentLink='ServiceBody_Statement' LowerBound='37.1' HigherBound='39.1'>
                    <om:Property Name='PortName' Value='SendGreetingsResponsePort' />
                    <om:Property Name='MessageName' Value='GreetingsResponseMsg' />
                    <om:Property Name='OperationName' Value='SendGreetingsResponseOperation' />
                    <om:Property Name='OperationMessageName' Value='Request' />
                    <om:Property Name='ReportToAnalyst' Value='True' />
                    <om:Property Name='Name' Value='Snd Greetings' />
                    <om:Property Name='Signal' Value='True' />
                </om:Element>
            </om:Element>
            <om:Element Type='PortDeclaration' OID='0d17c6cb-f6c9-439b-8366-f2e9fb18ccac' ParentLink='ServiceDeclaration_PortDeclaration' LowerBound='21.1' HigherBound='23.1'>
                <om:Property Name='PortModifier' Value='Implements' />
                <om:Property Name='Orientation' Value='Left' />
                <om:Property Name='PortIndex' Value='-1' />
                <om:Property Name='IsWebPort' Value='False' />
                <om:Property Name='OrderedDelivery' Value='False' />
                <om:Property Name='DeliveryNotification' Value='None' />
                <om:Property Name='Type' Value='DeploymentSampleApplication.ReceiveGreetingsPortType' />
                <om:Property Name='ParamDirection' Value='In' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='Name' Value='ReceiveGreetingsPort' />
                <om:Property Name='Signal' Value='False' />
                <om:Element Type='LogicalBindingAttribute' OID='cd4791ac-cbe8-4f70-9d0c-45baaf9d1478' ParentLink='PortDeclaration_CLRAttribute' LowerBound='21.1' HigherBound='22.1'>
                    <om:Property Name='Signal' Value='False' />
                </om:Element>
            </om:Element>
            <om:Element Type='PortDeclaration' OID='91940d8b-883e-4b20-9f74-11861a21c0a8' ParentLink='ServiceDeclaration_PortDeclaration' LowerBound='23.1' HigherBound='25.1'>
                <om:Property Name='PortModifier' Value='Uses' />
                <om:Property Name='Orientation' Value='Right' />
                <om:Property Name='PortIndex' Value='17' />
                <om:Property Name='IsWebPort' Value='False' />
                <om:Property Name='OrderedDelivery' Value='False' />
                <om:Property Name='DeliveryNotification' Value='None' />
                <om:Property Name='Type' Value='DeploymentSampleApplication.SendGreetingsResponsePortType' />
                <om:Property Name='ParamDirection' Value='In' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='Name' Value='SendGreetingsResponsePort' />
                <om:Property Name='Signal' Value='False' />
                <om:Element Type='LogicalBindingAttribute' OID='8de39679-3942-49da-a196-63b96a71c2ba' ParentLink='PortDeclaration_CLRAttribute' LowerBound='23.1' HigherBound='24.1'>
                    <om:Property Name='Signal' Value='False' />
                </om:Element>
            </om:Element>
            <om:Element Type='MessageDeclaration' OID='760cf65a-8aba-4019-828e-785687e244c6' ParentLink='ServiceDeclaration_MessageDeclaration' LowerBound='25.1' HigherBound='26.1'>
                <om:Property Name='Type' Value='DeploymentSampleApplication.GreetingsSchema' />
                <om:Property Name='ParamDirection' Value='In' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='Name' Value='GreetingsMsg' />
                <om:Property Name='Signal' Value='True' />
            </om:Element>
            <om:Element Type='MessageDeclaration' OID='f7ea0084-5bf1-46c5-b175-d350538368ad' ParentLink='ServiceDeclaration_MessageDeclaration' LowerBound='26.1' HigherBound='27.1'>
                <om:Property Name='Type' Value='DeploymentSampleApplication.GreetingsResponseSchema' />
                <om:Property Name='ParamDirection' Value='In' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='Name' Value='GreetingsResponseMsg' />
                <om:Property Name='Signal' Value='True' />
            </om:Element>
        </om:Element>
    </om:Element>
</om:MetaModel>
";

        [System.SerializableAttribute]
        public class __ProcessGreetingsOrchestration_root_0 : Microsoft.XLANGs.Core.ServiceContext
        {
            public __ProcessGreetingsOrchestration_root_0(Microsoft.XLANGs.Core.Service svc)
                : base(svc, "ProcessGreetingsOrchestration")
            {
            }

            public override int Index { get { return 0; } }

            public override Microsoft.XLANGs.Core.Segment InitialSegment
            {
                get { return _service._segments[0]; }
            }
            public override Microsoft.XLANGs.Core.Segment FinalSegment
            {
                get { return _service._segments[0]; }
            }

            public override int CompensationSegment { get { return -1; } }
            public override bool OnError()
            {
                Finally();
                return false;
            }

            public override void Finally()
            {
                ProcessGreetingsOrchestration __svc__ = (ProcessGreetingsOrchestration)_service;
                __ProcessGreetingsOrchestration_root_0 __ctx0__ = (__ProcessGreetingsOrchestration_root_0)(__svc__._stateMgrs[0]);

                if (__svc__.ReceiveGreetingsPort != null)
                {
                    __svc__.ReceiveGreetingsPort.Close(this, null);
                    __svc__.ReceiveGreetingsPort = null;
                }
                if (__svc__.SendGreetingsResponsePort != null)
                {
                    __svc__.SendGreetingsResponsePort.Close(this, null);
                    __svc__.SendGreetingsResponsePort = null;
                }
                base.Finally();
            }

            internal Microsoft.XLANGs.Core.SubscriptionWrapper __subWrapper0;
        }


        [System.SerializableAttribute]
        public class __ProcessGreetingsOrchestration_1 : Microsoft.XLANGs.Core.ExceptionHandlingContext
        {
            public __ProcessGreetingsOrchestration_1(Microsoft.XLANGs.Core.Service svc)
                : base(svc, "ProcessGreetingsOrchestration")
            {
            }

            public override int Index { get { return 1; } }

            public override bool CombineParentCommit { get { return true; } }

            public override Microsoft.XLANGs.Core.Segment InitialSegment
            {
                get { return _service._segments[1]; }
            }
            public override Microsoft.XLANGs.Core.Segment FinalSegment
            {
                get { return _service._segments[1]; }
            }

            public override int CompensationSegment { get { return -1; } }
            public override bool OnError()
            {
                Finally();
                return false;
            }

            public override void Finally()
            {
                ProcessGreetingsOrchestration __svc__ = (ProcessGreetingsOrchestration)_service;
                __ProcessGreetingsOrchestration_1 __ctx1__ = (__ProcessGreetingsOrchestration_1)(__svc__._stateMgrs[1]);

                if (__ctx1__ != null && __ctx1__.__GreetingsMsg != null)
                {
                    __ctx1__.UnrefMessage(__ctx1__.__GreetingsMsg);
                    __ctx1__.__GreetingsMsg = null;
                }
                if (__ctx1__ != null && __ctx1__.__GreetingsResponseMsg != null)
                {
                    __ctx1__.UnrefMessage(__ctx1__.__GreetingsResponseMsg);
                    __ctx1__.__GreetingsResponseMsg = null;
                }
                base.Finally();
            }

            [Microsoft.XLANGs.Core.UserVariableAttribute("GreetingsMsg")]
            public __messagetype_DeploymentSampleApplication_GreetingsSchema __GreetingsMsg;
            [Microsoft.XLANGs.Core.UserVariableAttribute("GreetingsResponseMsg")]
            public __messagetype_DeploymentSampleApplication_GreetingsResponseSchema __GreetingsResponseMsg;
        }

        private static Microsoft.XLANGs.Core.CorrelationType[] _correlationTypes = null;
        public override Microsoft.XLANGs.Core.CorrelationType[] CorrelationTypes { get { return _correlationTypes; } }

        private static System.Guid[] _convoySetIds;

        public override System.Guid[] ConvoySetGuids
        {
            get { return _convoySetIds; }
            set { _convoySetIds = value; }
        }

        public static object[] StaticConvoySetInformation
        {
            get {
                return null;
            }
        }

        [Microsoft.XLANGs.BaseTypes.LogicalBindingAttribute()]
        [Microsoft.XLANGs.BaseTypes.PortAttribute(
            Microsoft.XLANGs.BaseTypes.EXLangSParameter.eImplements
        )]
        [Microsoft.XLANGs.Core.UserVariableAttribute("ReceiveGreetingsPort")]
        internal ReceiveGreetingsPortType ReceiveGreetingsPort;
        [Microsoft.XLANGs.BaseTypes.LogicalBindingAttribute()]
        [Microsoft.XLANGs.BaseTypes.PortAttribute(
            Microsoft.XLANGs.BaseTypes.EXLangSParameter.eUses
        )]
        [Microsoft.XLANGs.Core.UserVariableAttribute("SendGreetingsResponsePort")]
        internal SendGreetingsResponsePortType SendGreetingsResponsePort;

        public static Microsoft.XLANGs.Core.PortInfo[] _portInfo = new Microsoft.XLANGs.Core.PortInfo[] {
            new Microsoft.XLANGs.Core.PortInfo(new Microsoft.XLANGs.Core.OperationInfo[] {ReceiveGreetingsPortType.RcvGreetingsOperation},
                                               typeof(ProcessGreetingsOrchestration).GetField("ReceiveGreetingsPort", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance),
                                               Microsoft.XLANGs.BaseTypes.Polarity.implements,
                                               false,
                                               Microsoft.XLANGs.Core.HashHelper.HashPort(typeof(ProcessGreetingsOrchestration), "ReceiveGreetingsPort"),
                                               null),
            new Microsoft.XLANGs.Core.PortInfo(new Microsoft.XLANGs.Core.OperationInfo[] {SendGreetingsResponsePortType.SendGreetingsResponseOperation},
                                               typeof(ProcessGreetingsOrchestration).GetField("SendGreetingsResponsePort", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance),
                                               Microsoft.XLANGs.BaseTypes.Polarity.uses,
                                               false,
                                               Microsoft.XLANGs.Core.HashHelper.HashPort(typeof(ProcessGreetingsOrchestration), "SendGreetingsResponsePort"),
                                               null)
        };

        public override Microsoft.XLANGs.Core.PortInfo[] PortInformation
        {
            get { return _portInfo; }
        }

        static public System.Collections.Hashtable PortsInformation
        {
            get
            {
                System.Collections.Hashtable h = new System.Collections.Hashtable();
                h[_portInfo[0].Name] = _portInfo[0];
                h[_portInfo[1].Name] = _portInfo[1];
                return h;
            }
        }

        public static System.Type[] InvokedServicesTypes
        {
            get
            {
                return new System.Type[] {
                    // type of each service invoked by this service
                };
            }
        }

        public static System.Type[] CalledServicesTypes
        {
            get
            {
                return new System.Type[] {
                };
            }
        }

        public static System.Type[] ExecedServicesTypes
        {
            get
            {
                return new System.Type[] {
                };
            }
        }

        public static object[] StaticSubscriptionsInformation {
            get {
                return new object[1]{
                     new object[5] { _portInfo[0], 0, null , -1, true }
                };
            }
        }

        public static Microsoft.XLANGs.RuntimeTypes.Location[] __eventLocations = new Microsoft.XLANGs.RuntimeTypes.Location[] {
            new Microsoft.XLANGs.RuntimeTypes.Location(0, "00000000-0000-0000-0000-000000000000", 1, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(1, "6df23575-3958-455e-8c01-4887d78d3bb9", 1, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(2, "6df23575-3958-455e-8c01-4887d78d3bb9", 1, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(3, "9481cdef-35eb-4f15-bfad-f0f322586dcb", 1, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(4, "9481cdef-35eb-4f15-bfad-f0f322586dcb", 1, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(5, "1944e460-8b7c-4d30-86e7-bf6a33c435b1", 1, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(6, "1944e460-8b7c-4d30-86e7-bf6a33c435b1", 1, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(7, "00000000-0000-0000-0000-000000000000", 1, false)
        };

        public override Microsoft.XLANGs.RuntimeTypes.Location[] EventLocations
        {
            get { return __eventLocations; }
        }

        public static Microsoft.XLANGs.RuntimeTypes.EventData[] __eventData = new Microsoft.XLANGs.RuntimeTypes.EventData[] {
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.Start | Microsoft.XLANGs.RuntimeTypes.Operation.Body),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.Start | Microsoft.XLANGs.RuntimeTypes.Operation.Receive),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.Start | Microsoft.XLANGs.RuntimeTypes.Operation.Construct),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.Start | Microsoft.XLANGs.RuntimeTypes.Operation.Send),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.Body)
        };

        public static int[] __progressLocation0 = new int[] { 0,0,0,7,7,};
        public static int[] __progressLocation1 = new int[] { 0,0,1,1,2,3,3,4,5,5,5,6,7,7,7,7,};

        public static int[][] __progressLocations = new int[2] [] {__progressLocation0,__progressLocation1};
        public override int[][] ProgressLocations {get {return __progressLocations;} }

        public Microsoft.XLANGs.Core.StopConditions segment0(Microsoft.XLANGs.Core.StopConditions stopOn)
        {
            Microsoft.XLANGs.Core.Segment __seg__ = _segments[0];
            Microsoft.XLANGs.Core.Context __ctx__ = (Microsoft.XLANGs.Core.Context)_stateMgrs[0];
            __ProcessGreetingsOrchestration_root_0 __ctx0__ = (__ProcessGreetingsOrchestration_root_0)_stateMgrs[0];
            __ProcessGreetingsOrchestration_1 __ctx1__ = (__ProcessGreetingsOrchestration_1)_stateMgrs[1];

            switch (__seg__.Progress)
            {
            case 0:
                ReceiveGreetingsPort = new ReceiveGreetingsPortType(0, this);
                SendGreetingsResponsePort = new SendGreetingsResponsePortType(1, this);
                __ctx__.PrologueCompleted = true;
                __ctx0__.__subWrapper0 = new Microsoft.XLANGs.Core.SubscriptionWrapper(ActivationSubGuids[0], ReceiveGreetingsPort, this);
                if ( !PostProgressInc( __seg__, __ctx__, 1 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                if ((stopOn & Microsoft.XLANGs.Core.StopConditions.Initialized) != 0)
                    return Microsoft.XLANGs.Core.StopConditions.Initialized;
                goto case 1;
            case 1:
                __ctx1__ = new __ProcessGreetingsOrchestration_1(this);
                _stateMgrs[1] = __ctx1__;
                if ( !PostProgressInc( __seg__, __ctx__, 2 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 2;
            case 2:
                __ctx0__.StartContext(__seg__, __ctx1__);
                if ( !PostProgressInc( __seg__, __ctx__, 3 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                return Microsoft.XLANGs.Core.StopConditions.Blocked;
            case 3:
                if (!__ctx0__.CleanupAndPrepareToCommit(__seg__))
                    return Microsoft.XLANGs.Core.StopConditions.Blocked;
                if ( !PostProgressInc( __seg__, __ctx__, 4 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 4;
            case 4:
                __ctx1__.Finally();
                ServiceDone(__seg__, (Microsoft.XLANGs.Core.Context)_stateMgrs[0]);
                __ctx0__.OnCommit();
                break;
            }
            return Microsoft.XLANGs.Core.StopConditions.Completed;
        }

        public Microsoft.XLANGs.Core.StopConditions segment1(Microsoft.XLANGs.Core.StopConditions stopOn)
        {
            Microsoft.XLANGs.Core.Envelope __msgEnv__ = null;
            Microsoft.XLANGs.Core.Segment __seg__ = _segments[1];
            Microsoft.XLANGs.Core.Context __ctx__ = (Microsoft.XLANGs.Core.Context)_stateMgrs[1];
            __ProcessGreetingsOrchestration_root_0 __ctx0__ = (__ProcessGreetingsOrchestration_root_0)_stateMgrs[0];
            __ProcessGreetingsOrchestration_1 __ctx1__ = (__ProcessGreetingsOrchestration_1)_stateMgrs[1];

            switch (__seg__.Progress)
            {
            case 0:
                __ctx__.PrologueCompleted = true;
                if ( !PostProgressInc( __seg__, __ctx__, 1 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 1;
            case 1:
                if ( !PreProgressInc( __seg__, __ctx__, 2 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[0],__eventData[0],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 2;
            case 2:
                if ( !PreProgressInc( __seg__, __ctx__, 3 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[1],__eventData[1],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 3;
            case 3:
                if (!ReceiveGreetingsPort.GetMessageId(__ctx0__.__subWrapper0.getSubscription(this), __seg__, __ctx1__, out __msgEnv__))
                    return Microsoft.XLANGs.Core.StopConditions.Blocked;
                if (__ctx1__.__GreetingsMsg != null)
                    __ctx1__.UnrefMessage(__ctx1__.__GreetingsMsg);
                __ctx1__.__GreetingsMsg = new __messagetype_DeploymentSampleApplication_GreetingsSchema("GreetingsMsg", __ctx1__);
                __ctx1__.RefMessage(__ctx1__.__GreetingsMsg);
                ReceiveGreetingsPort.ReceiveMessage(0, __msgEnv__, __ctx1__.__GreetingsMsg, null, (Microsoft.XLANGs.Core.Context)_stateMgrs[1], __seg__);
                if (ReceiveGreetingsPort != null)
                {
                    ReceiveGreetingsPort.Close(__ctx1__, __seg__);
                    ReceiveGreetingsPort = null;
                }
                if ( !PostProgressInc( __seg__, __ctx__, 4 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 4;
            case 4:
                if ( !PreProgressInc( __seg__, __ctx__, 5 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                {
                    Microsoft.XLANGs.RuntimeTypes.EventData __edata = new Microsoft.XLANGs.RuntimeTypes.EventData(Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.Receive);
                    __edata.Messages.Add(__ctx1__.__GreetingsMsg);
                    __edata.PortName = @"ReceiveGreetingsPort";
                    Tracker.FireEvent(__eventLocations[2],__edata,_stateMgrs[1].TrackDataStream );
                }
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 5;
            case 5:
                if ( !PreProgressInc( __seg__, __ctx__, 6 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[3],__eventData[2],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 6;
            case 6:
                {
                    __messagetype_DeploymentSampleApplication_GreetingsResponseSchema __GreetingsResponseMsg = new __messagetype_DeploymentSampleApplication_GreetingsResponseSchema("GreetingsResponseMsg", __ctx1__);

                    ApplyTransform(typeof(DeploymentSampleApplication.GreetingsToGreetingsResponseMap), new object[] {__GreetingsResponseMsg.part}, new object[] {__ctx1__.__GreetingsMsg.part});

                    if (__ctx1__.__GreetingsResponseMsg != null)
                        __ctx1__.UnrefMessage(__ctx1__.__GreetingsResponseMsg);
                    __ctx1__.__GreetingsResponseMsg = __GreetingsResponseMsg;
                    __ctx1__.RefMessage(__ctx1__.__GreetingsResponseMsg);
                }
                __ctx1__.__GreetingsResponseMsg.ConstructionCompleteEvent(true);
                if ( !PostProgressInc( __seg__, __ctx__, 7 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 7;
            case 7:
                if ( !PreProgressInc( __seg__, __ctx__, 8 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                {
                    Microsoft.XLANGs.RuntimeTypes.EventData __edata = new Microsoft.XLANGs.RuntimeTypes.EventData(Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.Construct);
                    __edata.Messages.Add(__ctx1__.__GreetingsResponseMsg);
                    __edata.Messages.Add(__ctx1__.__GreetingsMsg);
                    Tracker.FireEvent(__eventLocations[4],__edata,_stateMgrs[1].TrackDataStream );
                }
                if (__ctx1__ != null && __ctx1__.__GreetingsMsg != null)
                {
                    __ctx1__.UnrefMessage(__ctx1__.__GreetingsMsg);
                    __ctx1__.__GreetingsMsg = null;
                }
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 8;
            case 8:
                if ( !PreProgressInc( __seg__, __ctx__, 9 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[5],__eventData[3],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 9;
            case 9:
                if (!__ctx1__.PrepareToPendingCommit(__seg__))
                    return Microsoft.XLANGs.Core.StopConditions.Blocked;
                if ( !PostProgressInc( __seg__, __ctx__, 10 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 10;
            case 10:
                if ( !PreProgressInc( __seg__, __ctx__, 11 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                SendGreetingsResponsePort.SendMessage(0, __ctx1__.__GreetingsResponseMsg, null, null, __ctx1__, __seg__ , Microsoft.XLANGs.Core.ActivityFlags.NextActivityPersists );
                if (SendGreetingsResponsePort != null)
                {
                    SendGreetingsResponsePort.Close(__ctx1__, __seg__);
                    SendGreetingsResponsePort = null;
                }
                if ((stopOn & Microsoft.XLANGs.Core.StopConditions.OutgoingRqst) != 0)
                    return Microsoft.XLANGs.Core.StopConditions.OutgoingRqst;
                goto case 11;
            case 11:
                if ( !PreProgressInc( __seg__, __ctx__, 12 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                {
                    Microsoft.XLANGs.RuntimeTypes.EventData __edata = new Microsoft.XLANGs.RuntimeTypes.EventData(Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.Send);
                    __edata.Messages.Add(__ctx1__.__GreetingsResponseMsg);
                    __edata.PortName = @"SendGreetingsResponsePort";
                    Tracker.FireEvent(__eventLocations[6],__edata,_stateMgrs[1].TrackDataStream );
                }
                if (__ctx1__ != null && __ctx1__.__GreetingsResponseMsg != null)
                {
                    __ctx1__.UnrefMessage(__ctx1__.__GreetingsResponseMsg);
                    __ctx1__.__GreetingsResponseMsg = null;
                }
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 12;
            case 12:
                if ( !PreProgressInc( __seg__, __ctx__, 13 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[7],__eventData[4],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 13;
            case 13:
                if (!__ctx1__.CleanupAndPrepareToCommit(__seg__))
                    return Microsoft.XLANGs.Core.StopConditions.Blocked;
                if ( !PostProgressInc( __seg__, __ctx__, 14 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 14;
            case 14:
                if ( !PreProgressInc( __seg__, __ctx__, 15 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                __ctx1__.OnCommit();
                goto case 15;
            case 15:
                __seg__.SegmentDone();
                _segments[0].PredecessorDone(this);
                break;
            }
            return Microsoft.XLANGs.Core.StopConditions.Completed;
        }
    }

    [System.SerializableAttribute]
    sealed public class __DeploymentSampleApplication_GreetingsSchema__ : Microsoft.XLANGs.Core.XSDPart
    {
        private static DeploymentSampleApplication.GreetingsSchema _schema = new DeploymentSampleApplication.GreetingsSchema();

        public __DeploymentSampleApplication_GreetingsSchema__(Microsoft.XLANGs.Core.XMessage msg, string name, int index) : base(msg, name, index) { }

        
        #region part reflection support
        public static Microsoft.XLANGs.BaseTypes.SchemaBase PartSchema { get { return (Microsoft.XLANGs.BaseTypes.SchemaBase)_schema; } }
        #endregion // part reflection support
    }

    [Microsoft.XLANGs.BaseTypes.MessageTypeAttribute(
        Microsoft.XLANGs.BaseTypes.EXLangSAccess.ePublic,
        Microsoft.XLANGs.BaseTypes.EXLangSMessageInfo.eThirdKind,
        "DeploymentSampleApplication.GreetingsSchema",
        new System.Type[]{
            typeof(DeploymentSampleApplication.GreetingsSchema)
        },
        new string[]{
            "part"
        },
        new System.Type[]{
            typeof(__DeploymentSampleApplication_GreetingsSchema__)
        },
        0,
        @"http://DeploymentSampleApplication.GreetingsSchema#Greetings"
    )]
    [System.SerializableAttribute]
    sealed public class __messagetype_DeploymentSampleApplication_GreetingsSchema : Microsoft.BizTalk.XLANGs.BTXEngine.BTXMessage
    {
        public __DeploymentSampleApplication_GreetingsSchema__ part;

        private void __CreatePartWrappers()
        {
            part = new __DeploymentSampleApplication_GreetingsSchema__(this, "part", 0);
            this.AddPart("part", 0, part);
        }

        public __messagetype_DeploymentSampleApplication_GreetingsSchema(string msgName, Microsoft.XLANGs.Core.Context ctx) : base(msgName, ctx)
        {
            __CreatePartWrappers();
        }
    }

    [System.SerializableAttribute]
    sealed public class __DeploymentSampleApplication_GreetingsResponseSchema__ : Microsoft.XLANGs.Core.XSDPart
    {
        private static DeploymentSampleApplication.GreetingsResponseSchema _schema = new DeploymentSampleApplication.GreetingsResponseSchema();

        public __DeploymentSampleApplication_GreetingsResponseSchema__(Microsoft.XLANGs.Core.XMessage msg, string name, int index) : base(msg, name, index) { }

        
        #region part reflection support
        public static Microsoft.XLANGs.BaseTypes.SchemaBase PartSchema { get { return (Microsoft.XLANGs.BaseTypes.SchemaBase)_schema; } }
        #endregion // part reflection support
    }

    [Microsoft.XLANGs.BaseTypes.MessageTypeAttribute(
        Microsoft.XLANGs.BaseTypes.EXLangSAccess.ePublic,
        Microsoft.XLANGs.BaseTypes.EXLangSMessageInfo.eThirdKind,
        "DeploymentSampleApplication.GreetingsResponseSchema",
        new System.Type[]{
            typeof(DeploymentSampleApplication.GreetingsResponseSchema)
        },
        new string[]{
            "part"
        },
        new System.Type[]{
            typeof(__DeploymentSampleApplication_GreetingsResponseSchema__)
        },
        0,
        @"http://DeploymentSampleApplication.GreetingsResponseSchema#GreetingsResponse"
    )]
    [System.SerializableAttribute]
    sealed public class __messagetype_DeploymentSampleApplication_GreetingsResponseSchema : Microsoft.BizTalk.XLANGs.BTXEngine.BTXMessage
    {
        public __DeploymentSampleApplication_GreetingsResponseSchema__ part;

        private void __CreatePartWrappers()
        {
            part = new __DeploymentSampleApplication_GreetingsResponseSchema__(this, "part", 0);
            this.AddPart("part", 0, part);
        }

        public __messagetype_DeploymentSampleApplication_GreetingsResponseSchema(string msgName, Microsoft.XLANGs.Core.Context ctx) : base(msgName, ctx)
        {
            __CreatePartWrappers();
        }
    }

    [Microsoft.XLANGs.BaseTypes.BPELExportableAttribute(false)]
    sealed public class _MODULE_PROXY_ { }
}
