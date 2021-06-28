using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Contributors : MonoBehaviour
{
    private static List<ProjectContributor> contributorList = new List<ProjectContributor>()
    {
        new ProjectContributor()
        {
            Name = "Grygoriy Kulesko",
            Contributions = new string[]
            {
                "Project lead",
                "core gameplay",
                "laser functionality",
                "camera improvements",
                "ground asset", 
                "testing/debugging functionality",
                "initial laser visual",
                "optimizations",
                "assorted project organization to simplify collaboration",
                "difficulty tuning",
                "UX improvements for better game feel",
                "raw sound samples",
                "target hp display",
                "UI styling",
                "assorted bug fixes",
                "turret roof animation"
            }
        },
        new ProjectContributor()
        {
            Name = "Filipe Dottori",
            Contributions = new string[]
            {
                "Asteroid model",
                "bacterion model",
                "improved laser visual",
                "asteroid explosions",
                "enemy spawn/wave system",
                "heatwave functionality",
                "sky background",
                "heat UI",
                "change to universal render pipeline",
                "heat glow",
                "assorted bug fixes",
                "collision tuning",
                "bacterion burning",
            }
        },
        new ProjectContributor()
        {
            Name = "Will McKay",
            Contributions = new string[]
            {
                "Auto turrets",
                "bacterion functionality",
                "bacterion orbs attack",
                "asteroids motion and targeting",
                "settlement HP functionality and related UI",
                "end of round bonus",
                "scrap currency",
                "scrap reward popups",
                "wave completion message",
                "turret purchase and upgrade",
                "upgrade tool tips",
                "shield powerup and related upgrade support",
                "responsive UI scaling",
                "laser damage can be upgraded"
            }
        },
        new ProjectContributor()
        {
            Name = "Muhammed \"EyeForcz\" Durmusoglu",
            Contributions = new string[]
            {
                "Turret tower and related models/texturing/rigging/rigging",
                "laser ball"
            }
        },
        new ProjectContributor()
        {
            Name = "tiago.dev",
            Contributions = new string[]
            {
                "Sound fx manager",
                "game events",
                "start menu logic",
                "game intro",
                "programmatic animation",
                "camera shake"
            }
        },
        new ProjectContributor()
        {
            Name = "Vaan Hope Khani",
            Contributions = new string[]
            {
                "Laser sounds",
                "powerup acquired sound",
                "gameover sound"
            }
        },
        new ProjectContributor()
        {
            Name = "Chris DeLeon",
            Contributions = new string[]
            {
                "Voxel city skyscrapers",
                "heatwave visual",
                "game over screen",
                "compiled credits"
            }
        },
        new ProjectContributor()
        {
            Name = "Rami Bukhari",
            Contributions = new string[]
            {
                "Pause menu"
            }
        }
    };

    public static string GetCreditsText(
        string namePrefix = "•",
        string contributionSeparator = ", "
    )
    {
        StringBuilder builder = new StringBuilder();
        foreach(var contributor in contributorList)
        {

            builder.AppendLine(
                $"{namePrefix} {contributor.Name}: {string.Join(contributionSeparator, contributor.Contributions)}"
            );
        }

        return builder.ToString();
    }

    class ProjectContributor
    {
        public string Name;
        public string[] Contributions;
    }
}

