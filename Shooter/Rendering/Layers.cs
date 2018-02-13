﻿using System;
using System.Linq;
using ProductiveRage.Immutable;

namespace Raspware.Shooter.Rendering
{
	public sealed class Layers
	{
		private NonNullList<Layer> _layers { get; }

		public static Layers Instance { get; private set; } = null;

		private Layers(Resolution resolution) {
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));

			_layers = NonNullList.Of(
				new Layer(resolution, Id.Background, 1),
				new Layer(resolution, Id.Level, 2)
			).OrderBy(layer => layer.Order);
		}

		public static void ConfigureInstance(Resolution resolution)
		{
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));

			Instance = new Layers(resolution);
		}

		public Layer GetLayer(Id id)
		{
			var layer = _layers.Where(l => l.Id == id).FirstOrDefault();
			if (layer == null)
				throw new ArgumentNullException(nameof(layer));

			return layer;
		}

		public enum Id
		{
			Background,
			Level
		}
	}
}
