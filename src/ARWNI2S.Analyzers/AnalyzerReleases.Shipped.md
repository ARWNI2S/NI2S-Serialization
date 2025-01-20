## Release 3.3.0

### New Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|--------------------
ARWNI2S0003  | Usage   | Error  | Inherit from Actor

## Release 7.0.0

### New Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|--------------------
ARWNI2S0001  | Usage   | Error  | [AlwaysInterleave] must only be used on the actor interface method and not the actor class method
ARWNI2S0002  | Usage   | Error  | Reference parameter modifiers are not allowed
ARWNI2S0004  | Usage   | Info   | Add serialization [Id] and [NonSerialized] attributes
ARWNI2S0005  | Usage   | Info   | Add [GenerateSerializer] attribute to [Serializable] type
ARWNI2S0006  | Usage   | Error  | Abstract/serialized properties cannot be serialized
ARWNI2S0007  | Usage   | Error  | 
ARWNI2S0008  | Usage   | Error  | Actor interfaces cannot have properties
ARWNI2S0009  | Usage   | Error  | Actor interface methods must return a compatible type
ARWNI2S0010  | Usage   | Info   | Add missing [Alias] attribute
ARWNI2S0011  | Usage   | Error  | The [Alias] attribute must be unique to the declaring type
ARWNI2S0012  | Usage   | Error  | The [Id] attribute must be unique to each members of the declaring type
ARWNI2S0013  | Usage   | Error  | This attribute should not be used on actor implementations

### Removed Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|--------------------
ARWNI2S0003  | Usage   | Error  | Inherit from Actor