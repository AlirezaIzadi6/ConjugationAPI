﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToLearnApi.Models.Identity;

namespace ToLearnApi.Models.Flashcards.LearnAndReview;

public class Item
{
    public int Id { get; set; }
    public int DeckId { get; set; }
    [ForeignKey(nameof(CustomUser))]
    public required string UserId { get; set; }
    public CustomUser User { get; set; }
    public required bool Learned { get; set; }
    public required DateTime LearnedAt { get; set; }
    public required int NumberOfReviews { get; set; }
    public required DateTime LastReview {  get; set; }
    public required DateTime NextReview { get; set; }
    [Required]
    public int CardId { get; set; }
    public Card Card { get; set; }
}
