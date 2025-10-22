package com.example.streaming.models;

import java.util.List;

public class Playlist {
    private int id;
    private String title;
    private String description;
    private int creatorId;
    private String createdAt;
    private List<PlaylistItem> items;

    public int getId() {
        return id;
    }

    public String getTitle() {
        return title;
    }

    public String getDescription() {
        return description;
    }

    public List<PlaylistItem> getItems() {
        return items;
    }
}
