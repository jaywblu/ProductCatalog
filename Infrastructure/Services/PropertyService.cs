using Infrastructure.Entities;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class PropertyService (PropertyRepository propertyRepository)
{
    private readonly PropertyRepository _propertyRepository = propertyRepository;

    public Property CreateProperty(Property property)
    {
        return _propertyRepository.Create(property);
    }

    public Property GetProperty(Property property)
    {
        return _propertyRepository.Get(x => x.Title == property.Title && x.PropertyValue == property.PropertyValue);
    }

    public Property GetPropertyByTitle(string title)
    {
        return _propertyRepository.Get(x => x.Title == title);
    }

    public Property GetPropertyById(int id)
    {
        return _propertyRepository.Get(x => x.Id == id);
    }

    public IEnumerable<Property> GetAllProperties()
    {
        return _propertyRepository.GetAll();
    }

    public Property UpdateProperty(Property propertEntity)
    {
        var updatedProperty = _propertyRepository.Update(propertEntity, x => x.Id == propertEntity.Id);
        return updatedProperty;
    }

    public bool DeleteProperty(int id)
    {
        var result = _propertyRepository.Delete(x => x.Id == id);
        return result;
    }
}
