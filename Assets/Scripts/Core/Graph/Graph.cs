using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using C5;

namespace Core.Graph
{
    public abstract class Graph<TNode> where TNode : INode
    {
        protected abstract IEnumerable<TNode> GetNeighbours(TNode node);
        protected abstract IComparer<TNode> CreateComparer(params TNode[] args);

        /// <summary>
        ///     BFS + A-star
        /// </summary>
        /// <returns>First item - first step to end, do not equal start. End included as last item.</returns>
        public TNode[] GetPath(TNode start, TNode end)
        {
            var visited = new System.Collections.Generic.HashSet<TNode>(new[] {start});
            var queue = new Queue<TNode>(new []{start});
            var parents = new Dictionary<TNode, TNode>();

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                foreach (var neighbour in GetNeighbours(current))
                {
                    if (visited.Contains(neighbour)) continue;

                    parents[neighbour] = current;
                    if (neighbour.Equals(end))
                    {
                        var result = new List<TNode>(new[] {end});
                        while (!current.Equals(start))
                        {
                            result.Add(current);
                            current = parents[current];
                        }

                        result.Reverse();
                        return result.ToArray();
                    }

                    queue.Enqueue(neighbour);
                    visited.Add(neighbour);
                }

                visited.Add(current);
            }

            return Array.Empty<TNode>();
        }
    }
}