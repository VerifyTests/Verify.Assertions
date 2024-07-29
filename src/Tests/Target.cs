public record Target(Nested Nested);

public record Nested(string Property);

public record Inherited(string Property) :
    Nested(Property);