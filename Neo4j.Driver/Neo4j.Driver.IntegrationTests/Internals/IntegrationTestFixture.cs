﻿// Copyright (c) 2002-2019 "Neo4j,"
// Neo4j Sweden AB [http://neo4j.com]
// 
// This file is part of Neo4j.
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
using Neo4j.Driver.IntegrationTests.Internals;
using System;
using Xunit;
using static System.Boolean;
using static System.Environment;

namespace Neo4j.Driver.IntegrationTests
{
    public class StandAloneIntegrationTestFixture : IDisposable
    {
        public IStandAlone StandAlone { get; }

        private const string UsingLocalServer = "DOTNET_DRIVER_USING_LOCAL_SERVER_XXX";

        public StandAloneIntegrationTestFixture()
        {
            // If a system flag is set, then we use the local single server instead
            if (TryParse(GetEnvironmentVariable(UsingLocalServer), out _))
            {
                StandAlone = new LocalStandAloneInstance();
            }
            else
            {
                if (!BoltkitHelper.IsBoltkitAvailable())
                {
                    return;
                }

                try
                {
                    StandAlone = new StandAlone();
                }
                catch (Exception)
                {
                    Dispose();
                    throw;
                }
            }
        }

        public void Dispose()
        {
            StandAlone?.Dispose();
        }
    }

    public class CausalClusterIntegrationTestFixture : IDisposable
    {
        public CausalCluster Cluster { get; }

        public CausalClusterIntegrationTestFixture()
        {
            var isClusterSupported = BoltkitHelper.IsClusterSupported();
            if (!isClusterSupported.Item1)
            {
                return;
            }

            try
            {
                Cluster = new CausalCluster();
            }
            catch (Exception)
            {
                Dispose();
                throw;
            }
        }
        public void Dispose()
        {
            Cluster?.Dispose();
        }
    }

    [CollectionDefinition(CollectionName)]
    public class SAIntegrationCollection : ICollectionFixture<StandAloneIntegrationTestFixture>
    {
        public const string CollectionName = "StandAloneIntegration";
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

    [CollectionDefinition(CollectionName)]
    public class CCIntegrationCollection : ICollectionFixture<CausalClusterIntegrationTestFixture>
    {
        public const string CollectionName = "CausalClusterIntegration";
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
