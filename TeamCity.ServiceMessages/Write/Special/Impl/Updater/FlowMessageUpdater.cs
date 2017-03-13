/*
 * Copyright 2007-2017 JetBrains s.r.o.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace JetBrains.TeamCity.ServiceMessages.Write.Special.Impl.Updater
{
    using System;

    /// <summary>
    ///     Service message updater that adds FlowId to service message according to
    ///     http://confluence.jetbrains.net/display/TCD7/Build+Script+Interaction+with+TeamCity
    /// </summary>
    public class FlowMessageUpdater : IServiceMessageUpdater
    {
        private readonly IFlowIdGenerator _flowId;

        /// <summary>
        ///     Custructs updater
        /// </summary>
        /// <param name="flowId">flowId set to all messages</param>
        public FlowMessageUpdater([NotNull] string flowId)
        {
            if (flowId == null) throw new ArgumentNullException(nameof(flowId));
            FlowId = flowId;
        }

        /// <summary>
        ///     Creates flow id from given generator instance
        /// </summary>
        /// <param name="flowId"></param>
        public FlowMessageUpdater([NotNull] IFlowIdGenerator flowId) : this(flowId.NewFlowId())
        {
            if (flowId == null) throw new ArgumentNullException(nameof(flowId));
            _flowId = flowId;
        }

        public string FlowId { [NotNull] get; }

        public IServiceMessage UpdateServiceMessage(IServiceMessage message)
        {
            if (message.DefaultValue != null) return message;
            return new PatchedServiceMessage(message) {{"flowId", FlowId}};
        }
    }
}