using System.ComponentModel.DataAnnotations;

namespace LMSBackend.Domain.Enums
{
    public enum Affiliation
    {
        [Display(Name = "STUDENT")]
        student = 1,

        [Display(Name = "EMPLOYED")]
        employed = 2,

        [Display(Name = "UNEMPLOYED")]
        unemployed = 3,
        [Display(Name = "N/A")]
        notAvailable = 4
    }

    public enum Category
    {
        [Display(Name = "Truck")]
        Truck = 1,

        [Display(Name = "Car")]
        Car = 2,

        [Display(Name = "Motor")]
        Motor = 3,

        [Display(Name = "Bus")]
        Bus = 4,

        [Display(Name = "Multicab")]
        Multicab = 5,

        [Display(Name = "Tricycle")]
        Tricycle = 6,

        [Display(Name = "Commercial")]
        Commercial = 7,

        [Display(Name = "Residential")]
        Residential = 8,
        [Display(Name = "Lot")]
        Lot = 9
    }
}