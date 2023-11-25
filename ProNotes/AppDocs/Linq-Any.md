# Linq Any
The C# Linq Any Operator is used to check whether at least one of the elements of a data source satisfies a given condition or not. If any of the elements satisfy the given condition, then it returns true else return false. (The overload which takes a predicate as a parameter)

```csharp
int[] IntArray = { 11, 22, 33, 44, 55 };
var Result = IntArray.All(x => x > 10); // true since IntArray has at least one element greater than zero
```

It is also used to check whether a collection contains some data or not. That means it checks the length of the collection also. If it contains any data then it returns true else return false. (The overload which takes no parameter)

```csharp
int[] IntArray = { 11, 22, 33, 44, 55 };
var ResultMS = IntArray.Any(); // true since IntArray has at least one element
```

