﻿using commonItems;

namespace ImperatorToCK3.Imperator.Characters; 

public class CharacterAttributes {
	public int Martial { get; set; } = 0;
	public int Finesse { get; set; } = 0;
	public int Charisma { get; set; } = 0;
	public int Zeal { get; set; } = 0;

	private static CharacterAttributes parsedAttributes = new();
	private static readonly Parser parser = new();
	static CharacterAttributes() {
		parser.RegisterKeyword("martial", reader => parsedAttributes.Martial = reader.GetInt());
		parser.RegisterKeyword("finesse", reader => parsedAttributes.Finesse = reader.GetInt());
		parser.RegisterKeyword("charisma", reader => parsedAttributes.Charisma = reader.GetInt());
		parser.RegisterKeyword("zeal", reader => parsedAttributes.Zeal = reader.GetInt());
		parser.IgnoreAndLogUnregisteredItems();
	}
	public static CharacterAttributes Parse(BufferedReader reader) {
		parsedAttributes = new CharacterAttributes();
		parser.ParseStream(reader);
		return parsedAttributes;
	}
}