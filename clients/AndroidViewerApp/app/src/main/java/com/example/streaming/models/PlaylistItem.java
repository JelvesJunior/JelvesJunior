package com.example.streaming.models;

public class PlaylistItem {
    private int id;
    private int contentId;
    private String title;
    private String description;
    private String mediaUrl;
    private int order;

    public int getId() {
        return id;
    }

    public int getContentId() {
        return contentId;
    }

    public String getTitle() {
        return title;
    }

    public String getDescription() {
        return description;
    }

    public String getMediaUrl() {
        return mediaUrl;
    }

    public int getOrder() {
        return order;
    }
}
