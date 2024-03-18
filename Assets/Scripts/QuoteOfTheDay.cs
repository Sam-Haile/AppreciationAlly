using System;
using TMPro;
using UnityEngine;

public class QuoteOfTheDay : MonoBehaviour
{
    public TextMeshProUGUI quote;

    private int lastQuoteIndex = -1;
    string[] quotes = new string[]
    {
    "Gratitude turns what we have into enough.",
    "Be thankful for what you have; you'll end up having more.",
    "The root of joy is gratefulness.",
    "Gratitude is the fairest blossom which springs from the soul.",
    "Gratitude can transform common days into thanksgivings.",
    "When you are grateful, fear disappears and abundance appears.",
    "Gratitude is not only the greatest of virtues but the parent of all others.",
    "Happiness is not by chance, but by choice through gratitude.",
    "Give thanks for a little and you will find a lot.",
    "Gratitude makes sense of our past, brings peace for today, and creates a vision for tomorrow.",
    "The more you thank life, the more life gives you to be thankful for.",
    "Gratitude is the open door to abundance.",
    "The best way to pay for a lovely moment is to enjoy it with a grateful heart.",
    "A grateful mind is a great mind which eventually attracts to itself great things.",
    "Gratitude is the healthiest of all human emotions.",
    "Joy is the simplest form of gratitude.",
    "Being thankful is the quickest path to joy.",
    "Gratitude is riches. Complaint is poverty.",
    "Let us be grateful to people who make us happy.",
    "To live a life of gratitude is to catch a glimpse of heaven.",
    "Gratitude turns what we have into enough, and more.",
    "Appreciate everything, even the ordinary. Especially the ordinary.",
    "The essence of all beautiful art is gratitude.",
    "Gratitude is the music of the heart.",
    "A moment of gratitude makes a difference in your attitude.",
    "Gratitude is the sign of noble souls.",
    "Start each day with a positive thought and a grateful heart.",
    "Feeling gratitude and not expressing it is like wrapping a present and not giving it.",
    "Gratitude is a currency that we can mint for ourselves, and spend without fear of bankruptcy.",
    "Gratitude opens the door to the power, the wisdom, the creativity of the universe.",
    "Gratitude is a powerful catalyst for happiness.",
    "Gratitude changes the pangs of memory into a tranquil joy.",
    "The smallest act of kindness is worth more than the grandest intention.",
    "Count your blessings, not your problems.",
    "Gratitude is the most exquisite form of courtesy.",
    "Gratitude makes what we have enough.",
    "The secret to having it all is knowing you already do.",
    "When we focus on our gratitude, the tide of disappointment goes out and the tide of love rushes in.",
    "Gratitude is the memory of the heart.",
    "Be grateful for the tiny details of your life and make room for unexpected and beautiful blessings.",
    "Every day is a gift. But some days are packaged better.",
    "Gratitude doesn’t change the scenery. It merely washes clean the glass you look through so you can clearly see the colors.",
    "Practice gratitude to create your own sunshine.",
    "Gratitude is the wine for the soul. Go on. Get drunk.",
    "Thanksgiving is a joyous invitation to shower the world with love and gratitude.",
    "Gratitude is the reflection of kindness.",
    "Let gratitude be the pillow upon which you kneel to say your nightly prayer.",
    "Life is full of give and take. Give thanks and take nothing for granted.",
    "Being grateful does not mean that everything is necessarily good. It just means that you can accept it as a gift.",
    "Gratitude helps you to grow and expand; gratitude brings joy and laughter into your life and into the lives of all those around you.",
    "Nature does not hurry, yet everything is accomplished.",
    "Family is not an important thing. It's everything.",
    "True friends are like stars; you can only recognize them when it's dark around you.",
    "Kindness is the language which the deaf can hear and the blind can see.",
    "The Earth has music for those who listen.",
    "The love of a family is life's greatest blessing.",
    "Friends are the family we choose for ourselves.",
    "A single act of kindness throws out roots in all directions, and the roots spring up and make new trees.",
    "Look deep into nature, and then you will understand everything better.",
    "In family life, love is the oil that eases friction.",
    "Friendship is the only cement that will ever hold the world together.",
    "Kindness in words creates confidence. Kindness in thinking creates profoundness. Kindness in giving creates love.",
    "Every flower is a soul blossoming in nature.",
    "To us, family means putting your arms around each other and being there.",
    "A friend is one who knows you and loves you just the same.",
    "Wherever there is a human being, there is an opportunity for kindness.",
    "Nature is not a place to visit. It is home.",
    "Nothing is better than going home to family and eating good food and relaxing.",
    "Real friendship is when your friend comes over to your house and then you both just take a nap.",
    "Carry out a random act of kindness, with no expectation of reward, safe in the knowledge that one day someone might do the same for you.",
    "Nature always wears the colors of the spirit.",
    "The memories we make with our family is everything.",
    "A true friend never gets in your way unless you happen to be going down.",
    "Kindness is like snow—it beautifies everything it covers.",
    "The poetry of the earth is never dead.",
    "What can you do to promote world peace? Go home and love your family.",
    "Friends show their love in times of trouble, not in happiness.",
    "No act of kindness, no matter how small, is ever wasted.",
    "The best time to plant a tree was 20 years ago. The second best time is now.",
    "Family and friendships are two of the greatest facilitators of happiness.",
    "It's not what we have in life, but who we have in our life that matters.",
    "Unexpected kindness is the most powerful, least costly, and most underrated agent of human change.",
    "The ocean is a mighty harmonist.",
    "Family means no one gets left behind or forgotten.",
    "Life was meant for good friends and great adventures.",
    "Kindness begins with the understanding that we all struggle.",
    "To walk in nature is to witness a thousand miracles.",
    "Families are like branches on a tree. We grow in different directions yet our roots remain as one.",
    "A loyal friend laughs at your jokes when they're not so good, and sympathizes with your problems when they're not so bad.",
    "Kindness is choosing love over hate, light over darkness, compassion over judgment.",
    "Let us take our hearts for a walk in the woods and listen to the magic whispers of old trees.",
    "The bond that links your true family is not one of blood, but of respect and joy in each other's life.",
    "A friend is what the heart needs all the time.",
    "Always try to be a little kinder than is necessary.",
    "Adopt the pace of nature: her secret is patience.",
    "Spend time with family and friends, not money, on happiness.",
    "Friendship is the shadow of the evening, which increases with the setting sun of life.",
    "Kindness is the sunshine in which virtue grows.",
    "The beauty of the natural world lies in the details.",
    "In every conceivable manner, the family is link to our past, bridge to our future.",
    "Appreciation is like a flashlight in a dark room – it lights up what's already there.",
    "Saying 'thank you' is more than good manners. It's good spirituality.",
    "Appreciation can make a day, even change a life. Your willingness to put it into words is all that is necessary.",
    "If you look at what you have in life, you’ll always have more.",
    "The more you praise and celebrate your life, the more there is in life to celebrate.",
    "Appreciate the little things, for one day you may look back and realize they were the big things.",
    "Appreciation is a wonderful thing: It makes what is excellent in others belong to us as well.",
    "The best way to appreciate something is to be without it for a while.",
    "Take time to appreciate those who do so much for you but ask for nothing in return.",
    "When we give cheerfully and accept gratefully, everyone is blessed.",
    "Appreciation is the highest form of prayer, for it acknowledges the presence of good wherever you shine the light of your thankful thoughts.",
    "Sometimes the smallest act of love can take up the biggest space in someone's heart.",
    "Don't forget, a person's greatest emotional need is to feel appreciated.",
    "Appreciation and love are the same. When you appreciate someone deeply, you are showing them your love.",
    "Happiness doesn't result from what we get, but from what we give and appreciate in our lives.",
    "Show appreciation and for those you care, Let your love be known so they are aware.",
    "Every day, try to think of three things you are grateful for. It will change your day.",
    "Feeling gratitude and not expressing it is like wrapping a present and not giving it.",
    "Appreciate what you have, where you are, and who you are with in this moment.",
    "A little 'thank you' can make a big difference.",
    "Remember, we all stumble, every one of us. That's why it's a comfort to go hand in hand.",
    "When you appreciate the good, the good appreciates.",
    "Appreciation can make a day and even change a life. Your willingness to put it into words is all that is necessary.",
    "Give thanks for a little and you will find a lot.",
    "The secret to having it all, is knowing you already do.",
    "Appreciation is a free gift that you can give to anyone you encounter – it can change their day, and sometimes, even their life.",
    "Look for something positive in each day, even if some days you have to look a little harder.",
    "Let us be thankful for the fools. But for them the rest of us could not succeed.",
    "To express gratitude is gracious and honorable, to enact gratitude is generous and noble, but to live with gratitude ever in your heart is to touch heaven.",
    "Being thankful for each and every day is a sign of great understanding and maturity.",
    "Gratitude is the most important of all human emotions.",
    "An attitude of gratitude brings great things.",
    "You cannot do a kindness too soon because you never know how soon it will be too late.",
    "The art of being happy lies in the power of extracting happiness from common things.",
    "It’s not happiness that brings us gratitude. It’s gratitude that brings us happiness.",
    "Gratitude is the fairest blossom which springs from the soul.",
    "By practicing gratitude, we can enhance our emotional well-being and increase our happiness.",
    "Appreciation, respect, and love are the keys to a happy life.",
    "Count your rainbows, not your thunderstorms.",
    "Gratitude turns what we have into enough, and more.",
    "The magic of gratitude is that it shifts your perspective to such an extent that it changes the world you see."
    };

    private void Start()
    {
        DisplayRandomQuote();
    }

    void DisplayRandomQuote()
    {
        DateTime lastShownDate = GetLastShownDate();
        DateTime currentDate = DateTime.Now.Date; // Get current date with time stripped

        if (lastShownDate < currentDate)
        {
            int index = UnityEngine.Random.Range(0, quotes.Length);
            string randomQuote = quotes[index];
            quote.text = randomQuote; // Display the quote in the UI

            // Store the index of the selected quote
            PlayerPrefs.SetInt("LastQuoteIndex", index);
            UpdateLastShownDate(currentDate); // Update the last shown date
        }
        else
        {
            // It's the same day, so retrieve and display the last shown quote
            lastQuoteIndex = PlayerPrefs.GetInt("LastQuoteIndex", 0); // Default to 0 if not found
            quote.text = quotes[lastQuoteIndex];
        }
    }



    private DateTime GetLastShownDate()
    {
        string lastShownDateString = PlayerPrefs.GetString("LastShownDate", "");
        if (string.IsNullOrEmpty(lastShownDateString))
        {
            return DateTime.MinValue;
        }
        return DateTime.Parse(lastShownDateString);
    }

    private void UpdateLastShownDate(DateTime date)
    {
        PlayerPrefs.SetString("LastShownDate", date.ToString());
        PlayerPrefs.Save();
    }

}
