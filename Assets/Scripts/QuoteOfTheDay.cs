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
    "Being happy comes from being thankful.",
    "Being thankful can make ordinary days special.",
    "When you're thankful, you won't be scared, and you'll feel like you have more.",
    "Being thankful is the best thing you can do, and it leads to all other good things.",
    "Choosing to be happy and thankful is a decision we can all make.",
    "Even a little thank you can bring you a lot.",
    "Being thankful helps us feel good about our past, have peace today, and look forward to the future.",
    "The more you are thankful, the more you will find things to be thankful for.",
    "Being thankful opens the door to all good things.",
    "Enjoying a great moment is best done with a thankful heart.",
    "A thankful mind brings good things to your life.",
    "Being thankful is the best emotion.",
    "Simple joy comes from being thankful.",
    "Saying thanks is the fastest way to feel happy.",
    "Thankfulness brings you wealth, while complaining brings you nothing.",
    "Let's be thankful for the people who make us happy.",
    "Seeing the beauty in life comes from being thankful.",
    "Being thankful makes what we have more than enough.",
    "Notice and love the small things in life.",
    "The most beautiful art comes from being thankful.",
    "Thankfulness is like music for your heart.",
    "A moment of thankfulness can change how you see things.",
    "Thankful people are truly great.",
    "Start every day thinking something positive and feeling thankful.",
    "If you're thankful but don't show it, it's like wrapping a present but not giving it.",
    "Thankfulness is something we can all give, without ever running out.",
    "Thankfulness opens the door to all the good things in the world.",
    "Thankfulness is a key to happiness.",
    "Thankfulness turns sad memories into happy ones.",
    "Even the smallest kind act is better than the biggest good intention.",
    "Focus on what's good in your life, not the problems.",
    "Being thankful is the nicest thing you can be.",
    "Thankfulness makes what we have enough.",
    "The secret to having everything is realizing you already do.",
    "Focusing on being thankful makes us happier and brings more love into our lives.",
    "Thankfulness comes from the heart.",
    "Appreciate the small things and get ready for the good that's coming.",
    "Every day is special, but some are just wrapped better.",
    "Thankfulness lets us see the world in its true beauty.",
    "Being thankful makes your own sunshine.",
    "Thankfulness is like joy juice for your soul – enjoy it.",
    "Thanksgiving is a time to spread love and thankfulness around the world.",
    "Thankfulness shows you're kind.",
    "Say your night prayers with a thankful heart.",
    "Life is about giving and taking. Always give thanks and never take things for granted.",
    "Being thankful doesn't mean everything is perfect. It means you see it as a gift.",
    "Thankfulness helps us grow, brings joy, and makes everyone's life better.",
    "Nature teaches us to be patient and everything will happen in its own time.",
    "Family is everything.",
    "True friends shine brightest when it's dark.",
    "Kindness is understood by everyone.",
    "The Earth sings to those who listen.",
    "Family love is the best kind of love.",
    "Friends are the family you choose.",
    "One kind act leads to many more.",
    "Look closely at nature to understand life better.",
    "Love smooths out family life.",
    "Friendship keeps the world going.",
    "Kind words, thoughts, and gifts all show love.",
    "Every flower is a piece of nature's soul.",
    "Family means being there for each other.",
    "A friend loves you just the way you are.",
    "Every person is a chance to be kind.",
    "Nature is our home.",
    "Nothing beats family time, good food, and relaxation.",
    "Real friends are comfortable with quiet moments together.",
    "Do something kind without expecting anything back.",
    "Nature reflects our feelings.",
    "Making memories with family is priceless.",
    "A true friend helps you up when you're down.",
    "Kindness covers everything in beauty.",
    "Nature's poetry is eternal.",
    "Love your family to promote peace.",
    "Friends prove their love in tough times.",
    "Never underestimate a small act of kindness.",
    "The best time to plant a tree is now.",
    "Family and friends are the key to happiness.",
    "It's the people in our lives that make it worth living.",
    "Unexpected kindness can change everything.",
    "Family means everyone is included and remembered.",
    "Life is for making friends and having adventures.",
    "Kindness comes from understanding others' struggles.",
    "Walking in nature shows us miracles.",
    "Family grows in different directions but stays connected.",
    "Friends laugh and cry with you.",
    "Choose kindness, love, and light.",
    "Walking in the woods connects you to nature's magic.",
    "Family is about respect and joy in each other's lives",
    "A friend is what the heart needs all the time.",
    "Always try to be a little kinder than is necessary.",
    "Adopt the pace of nature: her secret is patience.",
    "Spend time with family and friends, not money, on happiness.",
    "Friendship grows stronger as we go through life together.",
    "Kindness helps all the good things in us grow like sunshine helps plants.",
    "Nature's beauty is all about the little details.",
    "Family connects our past and future in every way possible.",
    "Saying thank you is like turning on a light in a dark room – it shows us what's already there.",
    "Saying 'thank you' is not just polite, it's a way to make our hearts feel good.",
    "Saying thanks can make someone's day or even change their life. You just have to say it.",
    "If you're thankful for what you have, it'll always seem like you have more.",
    "The more you're happy about your life, the more you'll find things in life to be happy about.",
    "Love the small moments, because one day you might see they were the big moments.",
    "Saying thanks is awesome: it makes the good things in others feel like our own.",
    "Sometimes, we understand the value of something more when we don't have it for some time.",
    "Remember to say thanks to those who help you a lot, even when they don't ask for anything back.",
    "When we happily give and thankfully receive, everyone feels great.",
    "Being thankful is like a powerful thank-you note to the world, showing we see the good everywhere.",
    "Sometimes the smallest act of love can take up the biggest space in someone's heart.",
    "Remember, what we all need the most is to feel thanked and valued.",
    "Thanking someone and loving them is the same thing. When you really appreciate someone, it's a way of showing love.",
    "Being happy comes more from giving and being thankful than from just getting things.",
    "Make sure to show thanks and love to people you care about, so they know.",
    "Try to think of three things you're thankful for every day. It'll make your day better.",
    "Feeling gratitude and not expressing it is like wrapping a present and not giving it.",
    "Be thankful for what you have, where you are, and who you're with right now.",
    "A little 'thank you' can make a big difference.",
    "We all make mistakes. That's why it's nice to support each other.",
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
