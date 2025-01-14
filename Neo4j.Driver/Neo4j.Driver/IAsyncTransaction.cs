// Copyright (c) "Neo4j"
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

using System.Threading.Tasks;

namespace Neo4j.Driver
{
    /// <summary>
    /// Represents a transaction in the Neo4j database.
    /// </summary>
    public interface IAsyncTransaction : IAsyncQueryRunner
    {
        /// <summary>
        /// Asynchronously commit this transaction.
        /// </summary>
        /// <returns>A task of transaction commit.</returns>
        /// <exception cref="TransactionClosedException">Thrown when the transaction has previously been closed.</exception>
        Task CommitAsync();

        /// <summary>
        /// Asynchronously roll back this transaction.
        /// </summary>
        /// <returns>A task of transaction rollback.</returns>
        /// <exception cref="TransactionClosedException">>Thrown when the transaction has previously been closed.</exception>
        Task RollbackAsync();

        /// <summary>
        /// Gets the transaction configuration back.
        /// </summary>
        TransactionConfig TransactionConfig { get; }
    }
}