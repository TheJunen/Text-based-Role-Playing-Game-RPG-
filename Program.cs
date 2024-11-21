/*Tekstbasert Rollespill (RPG)
Lag et enkelt RPG-spill der spilleren har en karakter som kan gå på oppdrag, 
samle erfaring og forbedre ferdigheter basert på valg og interaksjoner.
Min definisjon av spillet:
 * Tekstbasert Rollespill (RPG) 
 * console app spill med en hovedperson som går igjennom forskjellige bosskamper og tjener coins og kan kjøpe i butikken
 * og får progresjon på historie % etter å slått bossen.
 * Får xp lvl etter å ha slått bane. Hvis du taper bane så mister du 50% penger eller xp
 * kan kjøpe sverd
 * Enkel form og grunnleggende funksjoner, bruk avanserte konsepter der det gir mening (se yt og internett)
*/
using TextBasedRolePlayGame.Items;
namespace TextBasedRolePlayGame
{
    class Program
    {
        static void Main(string[] args)
        {
            MainCharacter mainCharacter = new MainCharacter("Main character", 100, 1, 10, 10);
            Boss boss = new Boss("Boss lvl 1", 200, 2, 0, 10, 5, "Lvl 1 boss");
            Boss boss 
        }
    }
}