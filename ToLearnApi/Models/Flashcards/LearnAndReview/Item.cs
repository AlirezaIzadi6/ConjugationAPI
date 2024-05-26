﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToLearnApi.Models.Flashcards.LearnAndReview;

public class Item
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public bool Learned { get; set; }
    public int NumberOfReviews { get; set; }
    public DateTime LastReview {  get; set; }
    [Required]
    public int CardId { get; set; }
    public Card Card { get; set; }
}