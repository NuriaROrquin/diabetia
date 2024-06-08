using System;
namespace Diabetia.Domain.Entities.Events
{
	public class GenericEvent
	{
        public GenericEvent(){}

        GlucoseEvent glucoseEvent { get; set; }

	}
}

