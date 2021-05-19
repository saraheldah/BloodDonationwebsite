using BloodDonation.DataAccess.Entities;
using System.Collections.Generic;

namespace BloodDonation.DataAccess.Repositories
{
    public interface IDonationHistoryRepository
    {
        void Add(DonationHistory entity);
        
        IEnumerable<DonationHistory> All();
        
        void Delete(int id);
        
        void Delete(DonationHistory entity);
        
        DonationHistory Find(int id);
        
        DonationHistory FindByDonorId(int donorId);
        
        DonationHistory FindByRecipientId(int recipientId);
        
        void Update(DonationHistory entity);
    }
}