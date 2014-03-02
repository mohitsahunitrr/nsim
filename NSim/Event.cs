/*
 * Copyright (c) 2013, Jack Langman
 * Author: Jack Langman <youknowjack `AT` gmail.com>
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met: 
 * 
 * 1. Redistributions of source code must retain the above copyright notice, this
 *    list of conditions and the following disclaimer. 
 * 2. Redistributions in binary form must reproduce the above copyright notice,
 *    this list of conditions and the following disclaimer in the documentation
 *    and/or other materials provided with the distribution. 
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
 * ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */


using System;
using System.Collections.Generic;
using System.Linq;

namespace NSim
{
    internal class Event<T> : IEvent<T>
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IContext Context { get; private set; }
        public EventState State { get; private set; }

        public void Succeed()
        {
            throw new NotImplementedException();
        }

        public void Fail(Exception e)
        {
            throw new NotImplementedException();
        }

        public ICollection<Action<T>> Callbacks { get; private set; }

        public T Result { get; private set; }

        public void Succeed(T obj)
        {
            throw new NotImplementedException();
        }

        ICollection<Action> IEvent.Callbacks
        {
            get
            {
                return Callbacks
                    .Select(x => new Action(() => x(default(T))))
                    .ToArray();
            }
        }
    }
}