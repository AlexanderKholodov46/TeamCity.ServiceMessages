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

namespace JetBrains.TeamCity.ServiceMessages.Write.Special.Impl
{
    using System;
    using System.Globalization;
    using System.Threading;

    /// <summary>
    ///     Helper class to generate FlowIds
    /// </summary>
    public class DefaultFlowIdGenerator : IFlowIdGenerator
    {
        private int _ids;

        /// <summary>
        ///     Generates new unique FlowId
        /// </summary>
        public string NewFlowId()
        {
            return (
#if !NET35 && !NET40
                Interlocked.Increment(ref _ids)
#else
                Interlocked.Increment(ref _ids) << (27
                                                     + (Thread.CurrentThread.ManagedThreadId << 21)
                                                     + Environment.TickCount % int.MaxValue)
#endif
            ).ToString(CultureInfo.InvariantCulture);
        }
    }
}