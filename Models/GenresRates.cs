using ChineseNetflix.Models.Enums;

namespace ChineseNetflix.Models;

public struct GenresRates(float dramaRate, float comedyRate, float actionRate)
{
    public float DramaRate { get; private set; } = dramaRate;
    public float ComedyRate { get; private set; } = comedyRate;
    public float ActionRate { get; private set; } = actionRate;
}
