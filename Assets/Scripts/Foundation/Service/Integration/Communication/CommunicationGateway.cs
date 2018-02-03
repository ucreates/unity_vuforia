using UnityEngine;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using Service.Integration.Communication.Entity;
using Service.Integration.Communication.Client;
using Core.Device;
namespace Service.Integration.Communication {
public sealed class CommunicationGateway {
    private static CommunicationGateway instance {
        get;
        set;
    }
    private UserAgent userAgent {
        get;
        set;
    }
    private CommunicationGateway() {
        this.userAgent = new UserAgent();
    }
    public static CommunicationGateway GetInstance() {
        if (null == CommunicationGateway.instance) {
            CommunicationGateway.instance = new CommunicationGateway();
        }
        return CommunicationGateway.instance;
    }
    public void Request(CommunicationRequest parameter) {
        Thread t = new Thread(new ParameterizedThreadStart(AsyncRequest));
        parameter.threadId = t.ManagedThreadId;
        t.Start(parameter);
    }
    private void AsyncRequest(object parameter) {
        CommunicationRequest reqparam = parameter as CommunicationRequest;
        BaseCommunicationClient client =  CommunicationClientFactory.FactoryMethod(reqparam.clientType);
        client.userAgent = this.userAgent;
        if (null == client) {
            return;
        }
        CommunicationResponse response = client.Request(reqparam);
        lock (reqparam) {
            if (response.status == CommunicationResponse.ResponseStatus.SUCCESS) {
                reqparam.callback.OnSuccessCallback(response);
            } else {
                reqparam.callback.OnFaildCallback(response);
            }
        }
    }
}
}
