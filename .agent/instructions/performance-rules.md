# Performance Rules

Performance must be considered after every implementation.

For every new feature ask:

- Does this add unnecessary database calls?
- Is the same data being queried repeatedly?
- Is there an N+1 query?
- Can the query be projected?
- Should AsNoTracking be used?
- Is pagination needed?
- Is an index required?
- Is caching actually useful?
- Is Redis justified for this data?
- Is the response loading unnecessary data?
- Is JavaScript making duplicate requests?

Do not optimize blindly.

Only introduce Redis caching where repeated reads justify it.

Always prefer correctness first, then measure/improve actual bottlenecks.