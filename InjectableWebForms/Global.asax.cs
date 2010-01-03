﻿#region License

//
// Author: Javier Lozano <javier@lozanotek.com>
// Copyright (c) 2009-2010, lozanotek, inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

#endregion

namespace InjectableWebForms {
    using System;
    using System.Web;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Core;
    using Persistence;
    using Persistence.Impl;
    using Presenters;
    using Presenters.Impl;

    public class Global : HttpApplication {
        protected void Application_Start(object sender, EventArgs e) {
            IoC.Initialize();

            RegisterComponents();
        }

        protected void Application_End(object sender, EventArgs e) {
            if (IoC.WindsorContainer != null) {
                IoC.WindsorContainer.Dispose();
            }
        }

        private static void RegisterComponents() {
            IWindsorContainer container = IoC.WindsorContainer;

            container.Register(Component.For<IPersonRepository>()
                                   .ImplementedBy<StaticPersonRepository>()
                                   .LifeStyle.Transient,
                                   
                               Component.For<IPersonListPresenter>()
                                   .ImplementedBy<PersonListPresenter>()
                                   .LifeStyle.Transient,
                                   
                               Component.For<IPersonCreatorPresenter>()
                                   .ImplementedBy<PersonCreatorPresenter>()
                                   .LifeStyle.Transient);
        }
    }
}