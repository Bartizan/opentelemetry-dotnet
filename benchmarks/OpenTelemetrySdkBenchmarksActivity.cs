﻿// <copyright file="OpenTelemetrySdkBenchmarksActivity.cs" company="OpenTelemetry Authors">
// Copyright The OpenTelemetry Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

using System.Diagnostics;
using BenchmarkDotNet.Attributes;
using Benchmarks.Tracing;
using OpenTelemetry.Trace.Configuration;

namespace Benchmarks
{
    [MemoryDiagnoser]
    public class OpenTelemetrySdkBenchmarksActivity
    {
        private readonly ActivitySource BenchMarkSource = new ActivitySource("BenchMark");
        private readonly ActivityContext ParentCtx = new ActivityContext(ActivityTraceId.CreateRandom(), ActivitySpanId.CreateRandom(), ActivityTraceFlags.None);
        private readonly string ParentId = $"00-{ActivityTraceId.CreateRandom()}.{ActivitySpanId.CreateRandom()}.00";

        public OpenTelemetrySdkBenchmarksActivity()
        {
            // Not configuring pipeline, which will result in default NoOpActivityProcessor.
            var openTel = OpenTelemetrySdk.EnableOpenTelemetry(
                (builder) => builder.AddActivitySource("BenchMark")
                );
        }

        [Benchmark]
        public Activity CreateActivity_NoOpProcessor()
        {
            return ActivityCreationScenarios.CreateActivity(this.BenchMarkSource);
        }

        [Benchmark]
        public Activity CreateActivity_WithParentContext_NoOpProcessor()
        {
            return ActivityCreationScenarios.CreateActivityFromParentContext(this.BenchMarkSource, ParentCtx);
        }

        [Benchmark]
        public Activity CreateActivity_WithParentId_NoOpProcessor()
        {
            return ActivityCreationScenarios.CreateActivityFromParentId(this.BenchMarkSource, ParentId);
        }

        [Benchmark]
        public Activity CreateActivity_WithAttributes_NoOpProcessor()
        {
            return ActivityCreationScenarios.CreateActivityWithAttributes(this.BenchMarkSource);
        }

        [Benchmark]
        public Activity CreateActivity_WithAttributesAndCustomProp_NoOpProcessor()
        {
            return ActivityCreationScenarios.CreateActivityWithAttributesAndCustomProperty(this.BenchMarkSource);
        }
    }
}
