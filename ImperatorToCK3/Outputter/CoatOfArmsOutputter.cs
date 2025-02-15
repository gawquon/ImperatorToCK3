﻿using commonItems;
using commonItems.Mods;
using ImperatorToCK3.CK3.Dynasties;
using ImperatorToCK3.CK3.Titles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ImperatorToCK3.Outputter;
public static class CoatOfArmsOutputter {
	public static void OutputCoas(string outputModName, Title.LandedTitles titles, IEnumerable<Dynasty> dynasties) {
		Logger.Info("Outputting coats of arms...");
		var coasPath = Path.Combine("output", outputModName, "common", "coat_of_arms", "coat_of_arms");
		
		var path = Path.Combine(coasPath, "IRToCK3_coas.txt");
		using var coasWriter = new StreamWriter(path);
		
		// Output CoAs for titles.
		foreach (var title in titles) {
			var coa = title.CoA;
			if (coa is not null) {
				coasWriter.WriteLine($"{title.Id}={coa}");
			}
		}
		
		// Output CoAs for dynasties.
		foreach (var dynasty in dynasties.Where(d => d.CoA is not null)) {
			coasWriter.WriteLine($"{dynasty.Id}={dynasty.CoA}");
		}
		
		Logger.IncrementProgress();
	}

	public static void CopyCoaPatterns(ModFilesystem irModFS, string outputPath) {
		Logger.Info("Copying coats of arms patterns...");
		const string relativePatternsPath = "gfx/coat_of_arms/patterns";
		
		var filePaths = irModFS.GetAllFilesInFolderRecursive(relativePatternsPath);
		foreach (var filePath in filePaths) {
			var index = filePath.IndexOf(relativePatternsPath, StringComparison.Ordinal);
			var relativeFileOutputPath = filePath[index..];
			SystemUtils.TryCopyFile(filePath, Path.Combine(outputPath, relativeFileOutputPath));
		}
		
		Logger.IncrementProgress();
	}
}