using System;
namespace Diabetia.Domain.Entities.Events
{
	public class GenericEvent
	{
        public GenericEvent(){}

        public GlucoseEvent GlucoseEvent { get; set; }
        public InsulinEvent InsulinEvent { get; set; }
        public FoodEvent FoodEvent { get; set; }
        public ExamEvent ExamEvent { get; set; }
        public HealthEvent HealthEvent { get; set; }
        public MedicalVisitEvent MedicalVisitEvent { get; set; }
        public PhysicalActivityEvent PhysicalActivityEvent { get; set; }
        public int TypeEvent { get; set; }

	}
}

