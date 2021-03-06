﻿using FubuCore.Binding;
using FubuCore.Reflection;
using FubuMVC.Core.Diagnostics;
using FubuMVC.Core.Diagnostics.Tracing;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Tests.Diagnostics
{
    [TestFixture]
    public class RecordingPropertyBinderWrapperTester : InteractionContext<RecordingPropertyBinderWrapper>
    {
        [Test]
        public void should_resolve_binder_and_log_selection()
        {
            var property = ReflectionHelper.GetProperty<SimpleModel>(m => m.Name);

            MockFor<IDebugReport>()
                .Expect(r => r.AddBindingDetail(new PropertyBinderSelection
                                                    {
                                                        BinderType = typeof(NestedObjectPropertyBinder),
                                                        PropertyName = "Name",
                                                        PropertyType = typeof(string)
                                                    }));

            ClassUnderTest
                .BinderFor(property)
                .ShouldBeOfType<NestedObjectPropertyBinder>();

            VerifyCallsFor<IDebugReport>();
        }

        public class SimpleModel
        {
            public string Name { get; set; }
        }
    }
}