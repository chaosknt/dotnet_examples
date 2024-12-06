
string value1 = "NOELIA SOLEDAD DORST";
string value2 = "NOELIA SOLEDAD DORST ";


Console.WriteLine(value1.Trim().Equals(value2.Trim(), StringComparison.InvariantCultureIgnoreCase));