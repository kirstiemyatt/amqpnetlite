﻿//  ------------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation
//  All rights reserved. 
//  
//  Licensed under the Apache License, Version 2.0 (the ""License""); you may not use this 
//  file except in compliance with the License. You may obtain a copy of the License at 
//  http://www.apache.org/licenses/LICENSE-2.0  
//  
//  THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, 
//  EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR 
//  CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR 
//  NON-INFRINGEMENT. 
// 
//  See the Apache Version 2.0 License for specific language governing permissions and 
//  limitations under the License.
//  ------------------------------------------------------------------------------------

namespace Amqp.Sasl
{
    using System;
    using System.Text;

    sealed class SaslPlanProfile : SaslProfile
    {
        readonly string user;
        readonly string password;
        
        public SaslPlanProfile(string user, string password)
        {
            this.user = user;
            this.password = password;
        }

        protected override SaslInit GetInit(string hostname)
        {
            byte[] b1 = Encoding.UTF8.GetBytes(this.user);
            byte[] b2 = Encoding.UTF8.GetBytes(this.password);
            byte[] message = new byte[2 + b1.Length + b2.Length];
            Array.Copy(b1, 0, message, 1, b1.Length);
            Array.Copy(b2, 0, message, b1.Length + 2, b2.Length);

            SaslInit init = new SaslInit()
            {
                Mechanism = "PLAIN",
                InitialResponse = message,
                HostName = hostname
            };

            return init;
        }

        protected override SaslResponse OnChallenge(SaslChallenge challenge)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnOutcome(SaslOutcome outcome)
        {
        }
    }
}