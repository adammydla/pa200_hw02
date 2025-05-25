using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Sprint.DAL.EFCore.Models.Base;

namespace Sprint.DAL.EFCore.Models;

public class TrainerReservation : BaseEntity
{
    public Guid CourtReservationId { get; set; }

    [ForeignKey(nameof(CourtReservationId))]
    public virtual CourtReservation CourtReservation { get; set; }

    public Guid TrainerId { get; set; }

    [ForeignKey(nameof(TrainerId))]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public virtual Trainer Trainer { get; set; }
    
    public virtual TrainerReview? TrainerReview { get; set; }
}
